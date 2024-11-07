using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    private CharacterController2d characterController;

    [SerializeField] private Collider2D attackPoint;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private float attackTimer = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2d>();
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.fixedDeltaTime;
            }
            else
            {
                isAttacking = false;
                attackPoint.enabled = false;
            }
        }
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackCooldown;
            attackPoint.enabled = true;
        }
    }
}
