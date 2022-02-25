using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private GameModeSwitcher _gameModeSwitcher;
    [SerializeField] private SwipeInput _touchInput;
    [SerializeField] private float _lateralSpeed;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _leftRestriction, _rightRestriction;

    //private Player _player;
    private float _horizontalMove;
    private float _positionZ;

    private void Start()
    {
        _positionZ = transform.position.z;
    }

    //private void Awake()
    //{
    //    _player = GetComponent<Player>();
    //}

    private void OnEnable()
    {
        //_gameModeSwitcher.GameStarted += OnGameStarted;
        _touchInput.Touched += OnTouched;
        //_player.Died += OnDied;
    }

    private void OnDisable()
    {
        //_gameModeSwitcher.GameStarted -= OnGameStarted;
        _touchInput.Touched -= OnTouched;
        //_player.Died -= OnDied;
    }

    private void FixedUpdate()
    {
        _positionZ += _forwardSpeed * Time.deltaTime;
        transform.position = new Vector3(_horizontalMove * _lateralSpeed * Time.deltaTime, transform.localPosition.y, _positionZ);
    }



    private void OnTouched(float value)
    {
       // if( CanMove(value) == true)
            _horizontalMove += value;
    }



    private bool CanMove(float value)
    {
        if (value > 0 && transform.position.x > _leftRestriction)
            return true;
        else if (value < 0 && transform.position.x < _rightRestriction)
            return true;
        else
            return false;
    }
}
