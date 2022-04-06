using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private string _animationFallName = "Fall";
    private string _animationFallBackName = "FallBack";
    private string _animationClimbimgName = "Climb";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartFallAnimatoin()
    {
        _animator.SetTrigger(_animationFallName);
    }

    public void StartClimbAnimation()
    {
        _animator.SetBool(_animationClimbimgName, true);
    }

    public void EndClimbAnimation()
    {
        _animator.SetBool(_animationClimbimgName, false);
    }

    public void StartFallBackAnimatoin()
    {
        _animator.SetTrigger(_animationFallBackName);
    }
}
