using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    public event UnityAction<Obstacle> CollisedAnObstacle;
    public event UnityAction<Cylinder> CollisedAnCylinder;
    public event UnityAction<Lifting> CollisedAnLifting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cylinder cylinder))
        {
            CollisedAnCylinder?.Invoke(cylinder);
        }
        else if (other.TryGetComponent(out Obstacle obstacle))
        {
            CollisedAnObstacle?.Invoke(obstacle);
        }
        else if (other.TryGetComponent(out Lifting lifting))
        {
            CollisedAnLifting?.Invoke(lifting);
        }
    }
}
