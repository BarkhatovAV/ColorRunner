using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _controlCharacter = false;
    private string _axisName = "Mouse X";

    private bool _canMove => _controlCharacter == true;

    public event UnityAction<float> Touched;
    public event UnityAction Untouched;
    public event UnityAction ActivateMovement;

    private void Update()
    {
        if (_canMove == true)
        {

            Touched?.Invoke(Input.GetAxis(_axisName));
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

         ActivateMovement?.Invoke();

        
        _controlCharacter = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _controlCharacter = false;
        Untouched?.Invoke();
    }
}
