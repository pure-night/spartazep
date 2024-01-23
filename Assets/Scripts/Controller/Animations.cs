using UnityEngine;

public class Animations : MonoBehaviour
{
    protected Animator Animator;
    protected CharacterController Controller;

    protected void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
    }
}
