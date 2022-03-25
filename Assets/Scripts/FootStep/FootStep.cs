using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class FootStep : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _force = 150;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _renderer = gameObject.GetComponent<Renderer>();
    }

    public void SetMaterial(Material material)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _renderer.material = material;
    }

    public void Throw()
    {
        _animator.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.up * _force);
    }
}
