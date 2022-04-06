using System.Collections.Generic;
using UnityEngine;

public class FootStepContainer : MonoBehaviour
{
    [SerializeField] private FootStep _footStep;
    [SerializeField] private Transform _newParentTransformForFootsteps;

    private List<FootStep> _stairs = new List<FootStep>();
    private List<FootstepPlace> _footstepPlaces = new List<FootstepPlace>();
    private Material _currentMaterial;
    private Transform _startParent;
    private Vector3 _startLocalPosition;
    private float _obstacleHeight;
    
    public int FootstepsCount => _stairs.Count;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _footstepPlaces.Add(transform.GetChild(i).GetComponent<FootstepPlace>());
        }

        _startParent = transform.parent.transform;
        _startLocalPosition = transform.localPosition;
    }

    public void CreateFootStep()
    {
        FootStep footStep = Instantiate(_footStep, Vector3.zero, Quaternion.identity, _footstepPlaces[_stairs.Count].transform);
        footStep.SetMaterial(_currentMaterial);
        _stairs.Add(footStep);
    }

    public void SetMaterial(Material material)
    {
        _currentMaterial = material;

        foreach (FootStep footStep in _stairs)
        {
            footStep.SetMaterial(_currentMaterial);
        }
    }

    public bool IsHeighEnouth(Vector3 obstacle)
    {
        float obstacleHeightOffset = 1;
        _obstacleHeight = obstacle.y - obstacleHeightOffset;
        return _footstepPlaces[_stairs.Count].transform.position.y > _obstacleHeight;
    }

    public Vector3 GetLastFootstepPosition()
    {
        return _footstepPlaces[_stairs.Count].transform.position;
    }

    public void TransferStairsToPlayer()
    {
        int footstepQuantity = 0;
        int footstepCount = _stairs.Count;

        for (int footstepNumber = footstepCount - 1; footstepNumber > 0; footstepNumber--)
        {
            if (_footstepPlaces[footstepNumber].transform.position.y < _obstacleHeight)
            {
                RemoveFootStep(footstepNumber);
            }
            else
            {
                footstepQuantity++;
            }
        }

        transform.SetParent(_startParent);
        transform.localPosition = _startLocalPosition;

        for (int footstep = 0; footstep <= footstepQuantity; footstep++)
        {
            _stairs[footstep].transform.SetParent(_footstepPlaces[footstep].transform);
        }
    }

    public void RemoveAllFootsteps()
    {
        for (int footstepCount = _stairs.Count; footstepCount > 0 ; footstepCount--)
        {
            _stairs[_stairs.Count - 1].ThrowWithForce();
            _stairs[_stairs.Count - 1].transform.SetParent(_newParentTransformForFootsteps);
            _stairs.RemoveAt(_stairs.Count - 1);
        }
    }

    public void TransferStairsToNewParent()
    {
        transform.SetParent(_newParentTransformForFootsteps);
    }

    public void RemoveLastFootStep()
    {
        if (_stairs.Count > 0)
        {
            RemoveFootStep(_stairs.Count - 1);
        }
    }

    public void RemoveFootStep(int footStepNumber)
    {
        _stairs[footStepNumber].ThrowWithoutForce();
        _stairs[footStepNumber].transform.SetParent(_newParentTransformForFootsteps);
        _stairs.RemoveAt(footStepNumber);
    }
}
