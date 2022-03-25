using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerMaterial _playerMaterial;
    [SerializeField] private FootStepContainer _footStepContainer;
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnEnable()
    {
        _playerMovement.RiseFinished += OnRiseFinished;
        _playerMaterial.MaterialChanged += OnPlayerMaterialChanged;
    }

    private void OnPlayerMaterialChanged(Material material)
    {
        _footStepContainer.SetMaterial(material);
    }

    private void OnDisable()
    {
        _playerMovement.RiseFinished -= OnRiseFinished;
    }

    private void OnRiseFinished()
    {
        _footStepContainer.TakeLadder();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CylinderMaterial cylinder))
        {
            if (cylinder.MaterialNumer == _playerMaterial.MaterialNumber)
            {
                cylinder.Destroy();
                _footStepContainer.CreateFootStep();
            }
            else
            {
                _footStepContainer.RemoveLastFootStep();
            }
        }
        else if (other.TryGetComponent(out Obstacle obstacle))
        {
            _footStepContainer.RemoveAllFootsteps();
            _playerMovement.FallOverObstacle();
            
        }
        else if (other.TryGetComponent(out Lifting lifting))
        {
            
            if (_footStepContainer.IsHeighEnouth(lifting.TargetPosition))
            {
                _footStepContainer.CutOffLadder();
                _playerMovement.ClimbStairTo(lifting.TargetPosition);
            }
            else
            {
                _footStepContainer.CutOffLadder();
                _playerMovement.ClimbStairWithFall(lifting.TargetPosition,_footStepContainer.GetLastFootstepPosition());
            }
        }
    }
}
