using UnityEngine;
using DG.Tweening;

public class PlayerRotation : Rotation
{
    protected override void OnTouched(float value)
    {
        if (value > 0)
        {
            transform.DORotate(AngleOfRotation, RotateDuration);
        }
        else if (value < 0)
        {
            transform.DORotate(ReverseAngleOfRotation, RotateDuration);
        }
    }

    protected override void OnUntouched()
    {
        transform.DORotate(Vector3.zero, RotateDuration);
    }
}
