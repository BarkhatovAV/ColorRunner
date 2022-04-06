using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class FootStep : MonoBehaviour
{
    [SerializeField] private int _force;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _renderer = gameObject.GetComponent<Renderer>();
    }

    public void SetMaterial(Material material)
    {
        _renderer.material = material;
    }

    public void ThrowWithoutForce()
    {
        _animator.enabled = false;
        _rigidbody.isKinematic = false;
    }

    public void ThrowWithForce()
    {
        float maxTargetRotateX = 50;
        float minTargetRotateX = -50;
        float targetRotateX = Random.Range(maxTargetRotateX, minTargetRotateX);
        float maxTargetRotateY = 45;
        float minTargetRotateY = -45;
        float targetRotateY = Random.Range(maxTargetRotateY, minTargetRotateY);
        float maxTargetRotateZ = 8;
        float minTargetRotateZ = -8;
        float targetRotateZ = Random.Range(maxTargetRotateZ, minTargetRotateZ);
        Vector3 targetRotate = new Vector3(targetRotateX, targetRotateY, targetRotateZ);
        float duration = Random.Range(0.5f, 1);
        transform.DORotate(targetRotate, duration, RotateMode.Fast);

        float maxForce = _force + _force / 2;
        float minForce = _force - _force / 2;
        float force = Random.Range(maxForce, minForce);
        float VectorLengthY = Random.Range(-1, 1);
        float VectorLengthZ = 0.1f;
        _rigidbody.AddForce(new Vector3(0, VectorLengthY, VectorLengthZ) * force);

        _animator.enabled = false;
        _rigidbody.isKinematic = false;
    }
}
