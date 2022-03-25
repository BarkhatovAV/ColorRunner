using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private string _animationFallName = "Fall";
    private string _animationFallBackName = "FallBack";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayFallBackAimation(float delay)
    {
         Invoke(nameof(StartFallBackAnimatoin), delay);
    }

    public void StartFallAnimatoin()
    {
        _animator.SetTrigger(_animationFallName);
    }

    private void StartFallBackAnimatoin()
    {
        _animator.SetTrigger(_animationFallBackName);
    }
}
