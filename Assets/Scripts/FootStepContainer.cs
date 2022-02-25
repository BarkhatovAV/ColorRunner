using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepContainer : MonoBehaviour
{
    [SerializeField]  private FootStep _footStep;
    private List<FootStep> _ladder = new List<FootStep>();
    [SerializeField] private float _stepHeight;
    [SerializeField] private float _offsetY;
    [SerializeField] private Transform _transform;
    private float _positionY;
    private Material _currentMaterial;
    void Start()
    {

    }

    public void CreateFootStep()
    {
        _positionY = _stepHeight * _ladder.Count + _offsetY;
        Vector3 pos = transform.position;
        pos.y = _positionY;

        FootStep footStep = Instantiate(_footStep, pos, Quaternion.identity, transform);

        footStep.SetMaterial(_currentMaterial);
        _ladder.Add(footStep);


        
        //_ladder.Add(Instantiate(_footStep, pos, Quaternion.identity, transform));
        //_ladder[_ladder.Count - 1].SetMaterial(_currentMaterial);

    }

    public void SetMaterial(Material material)
    {
        
        _currentMaterial = material;

        Debug.Log("3" + _currentMaterial.color);
        foreach (FootStep footStep in _ladder)
        {
            footStep.SetMaterial(_currentMaterial);
        }
    }

    internal void RemoveFootStep()
    {
        if(_ladder.Count > 0)
        {
            _ladder[_ladder.Count - 1].Throw();
            _ladder[_ladder.Count - 1].transform.SetParent(_transform);
            _ladder.RemoveAt(_ladder.Count - 1);
        }
    }
}
