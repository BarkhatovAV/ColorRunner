using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FootStepContainer _footStepContainer;
    [SerializeField] private PlayerMaterial _playerMaterial;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private float _delayBeforeFalling;

    private void OnEnable()
    {
        _playerMovement.RiseFinished += OnRiseFinished;
        _playerMaterial.MaterialChanged += OnPlayerMaterialChanged;
        _playerCollision.CollisedAnCylinder += OnCollisedAnCylinder;
        _playerCollision.CollisedAnLifting += OnCollisedAnLifting;
        _playerCollision.CollisedAnObstacle += OnCollisedAnObstacle;
    }

    private void OnDisable()
    {
        _playerMovement.RiseFinished -= OnRiseFinished;
        _playerMaterial.MaterialChanged -= OnPlayerMaterialChanged;
        _playerCollision.CollisedAnCylinder -= OnCollisedAnCylinder;
        _playerCollision.CollisedAnLifting -= OnCollisedAnLifting;
        _playerCollision.CollisedAnObstacle -= OnCollisedAnObstacle;
    }

    private void OnCollisedAnObstacle(Obstacle obstacle)
    {
        _footStepContainer.RemoveAllFootsteps();

        StartCoroutine(Fall());
    }

    private void OnCollisedAnLifting(Lifting lifting)
    {
        if (_footStepContainer.IsHeighEnouth(lifting.TargetPosition))
        {
            _footStepContainer.TransferStairsToNewParent();
            _playerMovement.ClimbStairTo(lifting.TargetPosition);
        }
        else
        {
            _footStepContainer.TransferStairsToNewParent();
            _playerMovement.ClimbStairWithFall(lifting.TargetPosition, _footStepContainer.GetLastFootstepPosition());
        }
    }

    private void OnCollisedAnCylinder(Cylinder cylinder)
    {
        if (cylinder.MaterialNumer == _playerMaterial.MaterialNumber)
        {
            cylinder.Destroy();
            _footStepContainer.CreateFootStep();
        }
        else
        {
            if(_footStepContainer.FootstepsCount > 0)
            {
                _playerMovement.DecelerateVelocity();
                _footStepContainer.RemoveLastFootStep();
            }
        }
    }

    private void OnPlayerMaterialChanged(Material material)
    {
        _footStepContainer.SetMaterial(material);
    }

    private void OnRiseFinished()
    {
        _footStepContainer.TransferStairsToPlayer();
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(_delayBeforeFalling);
        _playerMovement.FallOverObstacle();
    }
}

