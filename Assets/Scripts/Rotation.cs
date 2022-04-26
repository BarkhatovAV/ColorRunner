using UnityEngine;
public abstract class Rotation : MonoBehaviour
{
    [SerializeField] protected TouchInput TouchInput;
    [SerializeField] protected float RotateDuration;
    [SerializeField] protected Vector3 AngleOfRotation;
    [SerializeField] protected Vector3 ReverseAngleOfRotation;

    private void OnEnable()
    {
        TouchInput.Touched += OnTouched;
        TouchInput.Untouched += OnUntouched;
    }

    private void OnDisable()
    {
        TouchInput.Touched -= OnTouched;
        TouchInput.Untouched -= OnUntouched;
    }

    protected abstract void OnTouched(float value);

    protected abstract void OnUntouched();
}
