using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private SwipeInput _touchInput;
    [SerializeField] private float _speed;
    [SerializeField] private float _delayBeforeStartRotationTurning;

    private float _lastDelayBeforeStartRotationTurning;
    private Vector3 _leftRotation = new Vector3(0, -60, 0);
    private Vector3 _rightRotation = new Vector3(0, 110, 0);
    private float _rotateY;
    private Quaternion _startRotation;
    private IEnumerator _coroutine;
    private bool _coroutineIsActive = false;

    private void Awake()
    {
        _lastDelayBeforeStartRotationTurning = _delayBeforeStartRotationTurning;
        _rotateY = transform.rotation.eulerAngles.y;
        _startRotation = Quaternion.Euler(transform.rotation.eulerAngles);
    }

    private void OnEnable()
    {
        _touchInput.Touched += OnTouched;
        _touchInput.Untouched += OnUntouched;
    }



    private void OnTouched(float value)
    {

            if (value > 0)
            {
                transform.rotation = Quaternion.Euler(0, 20, 0);
            }
            else if (value < 0)
            {
                transform.rotation = Quaternion.Euler(0, -20, 0);
            }
            //else
            //{
            //    transform.rotation = Quaternion.Euler(0, 0, 0);
            //}
    }

    private void OnDisable()
    {
        _touchInput.Touched -= OnTouched;
        _touchInput.Untouched -= OnUntouched;
    }

    private void OnUntouched()
    {
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }




    //private void OnTouched(float value)
    //{
    //    Debug.Log("value " + value + "transform.rotation.eulerAngles.y " + transform.rotation.eulerAngles.y + "_rightRotation.y " + _rightRotation.y);
    //    if (value > 0 && transform.rotation.eulerAngles.y < _rightRotation.y)
    //    {
    //        Rotate(true);
    //    }
    //    else if(value < 0 && transform.rotation.eulerAngles.y > _leftRotation.y)
    //    {
    //        Rotate(false);
    //        Debug.Log("value " + value + "transform.rotation.eulerAngles.y " + transform.rotation.eulerAngles.y + "_rightRotation.y " + _rightRotation.y);

    //    }
    //    else if(value == 0 && _lastDelayBeforeStartRotationTurning > 0 && transform.rotation != _startRotation)
    //    {


    //        _lastDelayBeforeStartRotationTurning -= Time.deltaTime;

    //        if (_lastDelayBeforeStartRotationTurning < 0)
    //            ReturnStartRotation();
    //    }
    //}

    //private void Rotate(bool rightSide)
    //{
    //    _rotateY += _speed * Time.deltaTime * (rightSide == true? 1 : -1);
    //    transform.rotation = Quaternion.Euler(0, _rotateY, 0);
    //}

    //private void ResetDelayBeforeReturnStartRotation()
    //{
    //    if(_lastDelayBeforeStartRotationTurning != _delayBeforeStartRotationTurning)
    //        _lastDelayBeforeStartRotationTurning = _delayBeforeStartRotationTurning;
    //}    

    //private void ReturnStartRotation()
    //{
    //    StopActiveCoroutine();

    //    _coroutine = StartPositionRotating(_startRotation);
    //    StartCoroutine(_coroutine);
    //}

    //private void OnUntouched()
    //{
    //    if(transform.rotation != _startRotation)
    //    {
    //        ReturnStartRotation();
    //    }
    //}

    //private IEnumerator StartPositionRotating(Quaternion startRotation)
    //{
    //    _coroutineIsActive = true;

    //    while (transform.rotation != startRotation)
    //    {
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, _speed * Time.deltaTime);
    //        _rotateY = transform.rotation.eulerAngles.y;
    //        yield return null;
    //    }

    //    ResetDelayBeforeReturnStartRotation();
    //    _coroutineIsActive = false;
    //}

    //private void StopActiveCoroutine()
    //{
    //    if (_coroutineIsActive == true)
    //    {
    //        StopCoroutine(_coroutine);
    //        _rotateY = transform.rotation.eulerAngles.y;
    //        ResetDelayBeforeReturnStartRotation();
    //        _coroutineIsActive = false;
    //    }
    //}
}
