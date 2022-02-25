using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class FootStep : MonoBehaviour
{
    //private Materials _gameMaterial;
    private Renderer _renderer;
    //[SerializeField] private Material _mat;
    private Rigidbody _rigidbody;
    //private 
    private Animator _animation;

    //private int _materialNumber;
    private void Awake()
    {
        _animation = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _renderer = gameObject.GetComponent<Renderer>();
        //_renderer.material = _mat;
        

    }



    public void SetMaterial(Material material)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        _renderer.material = material;
    }

    public void Throw()
    {

        _rigidbody.isKinematic = false;
            
    }




}
