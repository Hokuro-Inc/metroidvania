using UnityEngine;

[RequireComponent(typeof(PlayerBase))]
public class PlayerInput : MonoBehaviour
{
    private PlayerBase playerBase;

    private void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
    }

    public float HandleMovement()
    {
        float direction = 0f;

        if (Input.GetKey(InputManager.Instance.LeftKey) || Input.GetKey(InputManager.Instance.RightKey))
        {
            direction = Input.GetKey(InputManager.Instance.RightKey) ? 1f : -1f;
        }

        return direction;
    }

    public bool HandleJump()
    {
        return Input.GetKeyDown(InputManager.Instance.JumpKey) && playerBase.IsGrounded() && !playerBase.IsDashing() && !playerBase.IsCrouching();
    }

    public bool HandleDash()
    {
        return Input.GetKeyDown(InputManager.Instance.DashKey) && playerBase.IsGrounded() && !playerBase.IsDashing();
    }

    public bool HandleInteraction()
    {
        return Input.GetKeyDown(InputManager.Instance.InteractKey);
    }

    public bool HandleAttack()
    {
        return Input.GetKeyDown(InputManager.Instance.AttackKey) && !playerBase.IsCrouching() && playerBase.GetState() != State.attacking;
    }
}