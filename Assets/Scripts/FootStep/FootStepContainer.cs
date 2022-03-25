using System.Collections.Generic;
using UnityEngine;

public class FootStepContainer : MonoBehaviour
{
    [SerializeField] private FootStep _footStep;
    [SerializeField] private Transform _transform;

    private List<FootStep> _ladder = new List<FootStep>();
    private Material _currentMaterial;
    private Transform _startParent;
    private Vector3 _startLocalPosition;
    private float _obstacleHeight;
    private List<FootstepPlace> _footstepPlaces = new List<FootstepPlace>();
    private float _obstacleHeightOffset = 1;

    void Start()
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
        FootStep footStep = Instantiate(_footStep, Vector3.zero, Quaternion.identity, _footstepPlaces[_ladder.Count].transform);

        footStep.SetMaterial(_currentMaterial);
        _ladder.Add(footStep);
    }

    public void SetMaterial(Material material)
    {
        _currentMaterial = material;

        foreach (FootStep footStep in _ladder)
        {
            footStep.SetMaterial(_currentMaterial);
        }
    }

    public bool IsHeighEnouth(Vector3 obstacle)
    {
        _obstacleHeight = obstacle.y - _obstacleHeightOffset;
        return _footstepPlaces[_ladder.Count - 1].transform.position.y > _obstacleHeight;
    }

    public Vector3 GetLastFootstepPosition()
    {
        return _ladder[_ladder.Count - 1].transform.position;
    }

    public void TakeLadder()
    {
        int footstepQuantity = 0;
        int footstepCount = _ladder.Count;

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
            _ladder[footstep].transform.SetParent(_footstepPlaces[footstep].transform);
        }
    }

    public void CutOffLadder()
    {
        transform.SetParent(_transform);
    }

    public void RemoveLastFootStep()
    {
        if (_ladder.Count > 0)
        {
            RemoveFootStep(_ladder.Count - 1);
        }
    }

    public void RemoveFootStep(int footStepNumber)
    {
        _ladder[footStepNumber].Throw();
        _ladder[footStepNumber].transform.SetParent(_transform);
        _ladder.RemoveAt(footStepNumber);
    }

    public void RemoveAllFootsteps()
    {
        for (int footstepCount = _ladder.Count; footstepCount > 0 ; footstepCount--)
        {
            RemoveLastFootStep();
        }
    }
}
