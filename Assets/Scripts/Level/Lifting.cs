using UnityEngine;

public class Lifting : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    public Vector3 TargetPosition => _targetTransform.position;
}
