using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TouchInput _touchInput;
    [SerializeField] private float _lateralVelocity;
    [SerializeField] private float _fallDuration;
    [SerializeField] private float _climbingStairsDuration;
    [SerializeField] private float _forwardVelocity;
    [SerializeField] private float _decelerationDuration;
    [SerializeField] private float _smallForwardVelocity;
    [SerializeField] private float _fallVelocity;

    private float _startForwardVelocity;
    private PlayerAnimation _playerAnimation;
    private Vector3 _moveDirection;
    private float _horizontalMove;
    private float _maxDistant = 1000f;
    private float _fallOffset = 0.6f;
    private bool _isRunning = true;

    public event UnityAction RiseFinished;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _startForwardVelocity = _forwardVelocity;
    }

    private void OnEnable()
    {
        _touchInput.Touched += OnTouched;
    }

    private void OnDisable()
    {
        _touchInput.Touched -= OnTouched;
    }

    private void Update()
    {
        if (_isRunning)
        {
            RaycastHit hitInfo;
            Vector3 down = transform.TransformDirection(Vector3.down);
            Ray ray = new Ray(transform.position, down);

            if (Physics.Raycast(ray, out hitInfo, _maxDistant, _layerMask.value))
            {
                _moveDirection = new Vector3(_horizontalMove * _lateralVelocity, -hitInfo.normal.z, hitInfo.normal.y);
            }

            transform.Translate(_moveDirection * _forwardVelocity * Time.deltaTime);
            _horizontalMove = 0;
        }
    }

    public void DecelerateVelocity()
    {
        _forwardVelocity = _smallForwardVelocity;
        StartCoroutine(Decelerate(_decelerationDuration));
    }

    private void OnTouched(float value)
    {
        _horizontalMove += value;
    }

    public void ClimbStairTo(Vector3 targetPosition)
    {
        StartCoroutine(ClimbStairs(targetPosition));
    }

    public void ClimbStairWithFall(Vector3 targetPosition, Vector3 lastFootstepPosition)
    {
        _isRunning = false;

        Vector3 startPosition = transform.position;
        startPosition.y -= _fallOffset;
        float currentClimbingStairsDuration = _climbingStairsDuration / (targetPosition.y / lastFootstepPosition.y);
        transform.DOMove(new Vector3(transform.position.x, lastFootstepPosition.y, lastFootstepPosition.z),currentClimbingStairsDuration );

        float fallFromStairsDuration = currentClimbingStairsDuration / (targetPosition.y / lastFootstepPosition.y);
        transform.DOMove(startPosition, fallFromStairsDuration).SetDelay(currentClimbingStairsDuration);

        _playerAnimation.PlayFallBackAimation(currentClimbingStairsDuration);
    }

    public void FallOverObstacle()
    {
        float y = transform.position.y;
        transform.DOMoveY(y-0.5f, _fallDuration / 2);
        transform.DOMoveY(y, _fallDuration / 2).SetDelay(_fallDuration);
        _playerAnimation.StartFallAnimatoin();
        _forwardVelocity = _fallVelocity;
        StartCoroutine(Decelerate(_fallDuration));
    }

    private IEnumerator Decelerate(float duration)
    {
        yield return new WaitForSeconds(duration);
        _forwardVelocity = _startForwardVelocity;
    }

    private IEnumerator ClimbStairs(Vector3 targetPosition)
    {
        _isRunning = false;
        Vector3 startPosition = transform.position;
        float currentTime = 0;

        while(currentTime <_climbingStairsDuration)
        {
            currentTime += Time.deltaTime;
            float normalizedTime = currentTime / _climbingStairsDuration;
            transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, targetPosition.y, targetPosition.z), normalizedTime);
            yield return null;
        }

        RiseFinished?.Invoke();
        _isRunning = true;
    }
}
