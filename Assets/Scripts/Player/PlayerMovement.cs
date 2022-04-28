using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TouchInput _touchInput;
    [SerializeField] private float _lateralVelocity;
    [SerializeField] private float _forwardVelocity;
    [SerializeField] private float _wrongCylinderForwardVelocity;
    [SerializeField] private float _fallVelocity;
    [SerializeField] private float _fallDuration;
    [SerializeField] private float _climbingStairsDuration;
    [SerializeField] private float _decelerationDuration;
    
    private PlayerAnimator _playerAnimation;
    private Vector3 _moveDirection;
    private float _durationOfReductionSpeedBeforeFalling = 0.9f;
    private float _currentTime = 0;
    private float _startForwardVelocity;
    private float _horizontalMove;
    private float _maxDistant = 1000f;
    private bool _isRunning = true;


    public event Action RiseFinished;
    public event Action FellDownStairs;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimator>();
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
        _forwardVelocity = _wrongCylinderForwardVelocity;
        StartCoroutine(RestoreStartSpeed(_decelerationDuration));
    }

    private void OnTouched(float value)
    {
        _horizontalMove += value;
    }

    public void ClimbStairTo(Vector3 targetPosition)
    {
        StartCoroutine(ClimbStairs(targetPosition));
        _playerAnimation.StartClimbAnimation();
    }

    public void ClimbStairWithFall(Vector3 targetPosition, Vector3 lastFootstepPosition)
    {
        _isRunning = false;
        _playerAnimation.StartClimbAnimation();
        float fallOffsetY = 0.6f;
        float fallOffsetZ = 3.5f;
        
        Vector3 targetPositionForFall = new Vector3(transform.position.x, transform.position.y - fallOffsetY, transform.position.z - fallOffsetZ);
        
        float currentClimbingStairsDuration = _climbingStairsDuration / (targetPosition.y / lastFootstepPosition.y);
        float climbingStairsOffsetZ = 0.4f;
        transform.DOMove(new Vector3(transform.position.x, lastFootstepPosition.y, lastFootstepPosition.z - climbingStairsOffsetZ),currentClimbingStairsDuration );

        transform.DOMove(targetPositionForFall, currentClimbingStairsDuration).SetDelay(currentClimbingStairsDuration);
        StartCoroutine(FallDownStairs(currentClimbingStairsDuration));
    }

    public void FallOverObstacle()
    {
        float loweringDuratoin = 0.5f;
        float riseDuration = 0.2f;
        float loweringDistance = 0.65f;
        float positionY = transform.position.y;
        float delayBeforeRise = 0.07f;
        transform.DOMoveY(positionY - loweringDistance, loweringDuratoin).SetDelay(loweringDuratoin) ;
        transform.DOMoveY(positionY, riseDuration).SetDelay(_fallDuration - delayBeforeRise);
        _playerAnimation.StartFallAnimatoin();

        _currentTime = 0;
        StartCoroutine(ReduceSpeedBeforeFalling());
        StartCoroutine(RestoreStartSpeed(_fallDuration));
    }

    private IEnumerator ReduceSpeedBeforeFalling()
    {
        while(_currentTime < _durationOfReductionSpeedBeforeFalling)
        {
            _currentTime += Time.deltaTime;
            float normalizedTime = _currentTime / _durationOfReductionSpeedBeforeFalling;
            _forwardVelocity = Mathf.Lerp(_forwardVelocity, _fallVelocity, normalizedTime);
            yield return null;
        }
    }

    private IEnumerator RestoreStartSpeed(float duration)
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
        _playerAnimation.EndClimbAnimation();
    }

    private IEnumerator FallDownStairs(float climbingStairsDuration)
    {
        yield return new WaitForSeconds(climbingStairsDuration);

        _playerAnimation.StartFallBackAnimatoin();
        RiseFinished?.Invoke();
        FellDownStairs?.Invoke();
    }
}
