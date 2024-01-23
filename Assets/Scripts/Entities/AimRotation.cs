using UnityEngine;

public class AimRotation : MonoBehaviour
{
    public SpriteRenderer characterRenderer;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        RotateArm(newAimDirection);
    }

    private void RotateArm(Vector2 direction)
    {
        var rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var isFlip = Mathf.Abs(rotZ) > 90f;
        
        characterRenderer.flipX = isFlip;
    }
}
