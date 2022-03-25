using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _canMove = false;
    private string _axisName = "Mouse X";

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
        _canMove = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _canMove = false;
        Untouched?.Invoke();
    }
}
