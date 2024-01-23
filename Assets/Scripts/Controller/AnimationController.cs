using UnityEngine;

public class AnimationController : Animations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private void Start()
    {
        Controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 obj)
    {
        Animator.SetBool(IsWalking, obj.magnitude > .5f);
    }
}
