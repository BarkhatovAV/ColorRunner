using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StripeMaterial : MonoBehaviour
{
    [SerializeField] private Materials _gameMaterial;
    [SerializeField] private int _materialNumber;
    [SerializeField] private Renderer _renderer;

    
    private void Start()
    {
        _renderer.material = _gameMaterial.GetMaterial(_materialNumber);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMaterial _playerMaterial))
        {
            _playerMaterial.ChangeMaterial(_materialNumber);

        }
    }



}
