using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private TouchInput _touchInput;

    private void OnEnable()
    {
        _touchInput.Touched += OnTouched;
        _touchInput.Untouched += OnUntouched;
    }

    private void OnTouched(float value)
    {
        float rotationAngle = 10;


        if (value > 0)
        {
            transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
        }
        else if (value < 0)
        {
            transform.rotation = Quaternion.Euler(0, -rotationAngle, 0);
        }
    }

    private void OnDisable()
    {
        _touchInput.Touched -= OnTouched;
        _touchInput.Untouched -= OnUntouched;
    }

    private void OnUntouched()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
