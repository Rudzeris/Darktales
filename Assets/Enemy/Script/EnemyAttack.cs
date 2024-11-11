using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1.5f; // Время между атаками
    [SerializeField] private float attackDuration = 0.2f; // Длительность атаки
    [SerializeField] private int attackDamage = 5; // Урон от атаки

    private float attackCooldownTimer = 0f;
    private bool isAttacking = false;

    private void Awake()
    {
    }

    private void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
    }

    private void StopAttack()
    {
        isAttacking = false;
        attackCooldownTimer = attackCooldown;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackCooldownTimer <= 0 && collision.CompareTag("Player"))
        {
            StartAttack();

            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(attackDamage);
            }

            attackCooldownTimer = attackCooldown;
        }
    }
}
