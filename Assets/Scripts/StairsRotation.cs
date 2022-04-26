using UnityEngine;
using DG.Tweening;

public class StairsRotation : Rotation
{
    protected override void OnTouched(float value)
    {
        if (value > 0)
        {
            transform.DOLocalRotate(AngleOfRotation, RotateDuration);
        }
        else if (value < 0)
        {
            transform.DOLocalRotate(ReverseAngleOfRotation, RotateDuration);
        }
    }

    protected override void OnUntouched()
    {
        Vector3 startRotation = new Vector3(8, 0, 0);
        transform.DOLocalRotate(startRotation, RotateDuration);
    }
}
