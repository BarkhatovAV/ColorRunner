using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    //[SerializeField] private PlayerMaterial _playerMaterial;
    //[SerializeField] private FootStepContainer _footStepContainer;
    //[SerializeField] private PlayerMovement _playerMovement;

    //private void OnEnable()
    //{
    //    _playerMovement.RiseFinished += OnRiseFinished;
    //    _playerMaterial.MaterialChanged += OnPlayerMaterialChanged;
    //}

    //private void OnPlayerMaterialChanged(Material material)
    //{
    //    _footStepContainer.SetMaterial(material);
    //}

    //private void OnDisable()
    //{
    //    _playerMovement.RiseFinished -= OnRiseFinished;
    //}

    //private void OnRiseFinished()
    //{
    //    _footStepContainer.TakeLadder();
    //}

    public event UnityAction<Obstacle> CollisedAnObstacle;
    public event UnityAction<Cylinder> CollisedAnCylinder;
    public event UnityAction<Lifting> CollisedAnLifting;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cylinder cylinder))
        {
            CollisedAnCylinder?.Invoke(cylinder);
            Debug.Log("1");
            //if (cylinder.MaterialNumer == _playerMaterial.MaterialNumber)
            //{
            //    cylinder.Destroy();
            //    _footStepContainer.CreateFootStep();
            //}
            //else
            //{
            //    _playerMovement.DecelerateVelocity();
            //    _footStepContainer.RemoveLastFootStep();
            //}
        }
        else if (other.TryGetComponent(out Obstacle obstacle))
        {
            CollisedAnObstacle?.Invoke(obstacle);
            //_footStepContainer.RemoveAllFootsteps();
            //_playerMovement.FallOverObstacle();

        }
        else if (other.TryGetComponent(out Lifting lifting))
        {
            CollisedAnLifting?.Invoke(lifting);
            //if (_footStepContainer.IsHeighEnouth(lifting.TargetPosition))
            //{
            //    _footStepContainer.CutOffLadder();
            //    _playerMovement.ClimbStairTo(lifting.TargetPosition);
            //}
            //else
            //{
            //    _footStepContainer.CutOffLadder();
            //    _playerMovement.ClimbStairWithFall(lifting.TargetPosition,_footStepContainer.GetLastFootstepPosition());
            //}
        }
    }
}
