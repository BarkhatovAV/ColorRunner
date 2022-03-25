using UnityEngine;

public class Lifting : MonoBehaviour
{
    [SerializeField] private float height;
    [SerializeField] private Transform _targetTransform;

    public float Height => height;
    public Vector3 TargetPosition => _targetTransform.position;
}
