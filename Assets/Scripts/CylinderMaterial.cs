using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMaterial : MonoBehaviour
{
    [SerializeField] private Materials _gameMaterial;
    [SerializeField] private int _materialNumber;
    [SerializeField] private Renderer _renderer;

    public int MaterialNumer => _materialNumber;

    private void Start()
    {
        _renderer.material = _gameMaterial.GetMaterial(_materialNumber);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent(out PlayerMaterial _playerMaterial))
    //    {
    //        if (_playerMaterial.ColorNumber == _materialNumber)
    //        {

    //            Destroy(gameObject);

    //        }
    //    }
    //}
}
