using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1.5f; // Время между атаками
    [SerializeField] private float attackDuration = 1f; // Длительность атаки
    [SerializeField] private int attackDamage = 5; // Урон от атаки
    [SerializeField] private Animator animator;

    private float attackCooldownTimer = 0f;
    private bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        animator?.SetBool("IsAttacking", true);
        Invoke("StopAttack", attackDuration);
    }

    private void StopAttack()
    {
        isAttacking = false;
        animator?.SetBool("IsAttacking", false);
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
