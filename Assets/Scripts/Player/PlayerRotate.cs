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
        if (value > 0)
        {
            transform.rotation = Quaternion.Euler(0, 15, 0);
        }
        else if (value < 0)
        {
            transform.rotation = Quaternion.Euler(0, -15, 0);
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
