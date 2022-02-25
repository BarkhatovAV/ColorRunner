using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMaterial : MonoBehaviour
{
    [SerializeField] private Materials _materials;
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private Color _startColor;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private FootStepContainer _footStepContainer;

    private int _materialNumber;
    public int MaterialNumber => _materialNumber;

    public event UnityAction<Material> MaterialChanged;

    private void Start()
    {
        _playerMaterial.color = _startColor;
    }

    public void ChangeMaterial(int materialNumber)
    {
        _renderer.material = _materials.GetMaterial(materialNumber);
        _materialNumber = materialNumber;

        _footStepContainer.SetMaterial(_materials.GetMaterial(materialNumber));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CylinderMaterial _cylinder))
        {
            if (_cylinder.MaterialNumer == _materialNumber)
            {
                _cylinder.Destroy();
                _footStepContainer.CreateFootStep();
            }
            else
            {
                _footStepContainer.RemoveFootStep();
            }
        }
    }
}
