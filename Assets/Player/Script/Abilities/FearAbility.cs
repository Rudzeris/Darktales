using Assets.Player.Script.Abilities;
using UnityEditor.Build;
using UnityEngine;

public class FearAbility : MonoBehaviour, IAbility
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float stunDuration = 5f;
    [SerializeField] private float radius = Screen.width / 4;
    [SerializeField] private float cooldown = 20f;

    [SerializeField] private float cooldownTimer = 0f;

    public float Cooldown { get { return cooldown; } }

    public void Activate()
    {
        if (cooldownTimer > 0)
        {
            Debug.Log($"Ability is on cooldown. Wait {cooldownTimer} sec");
            return;
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);
        Collider2D enemyCollider = null;
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy") && 
                (enemyCollider is null || 
                Vector3.Distance(enemy.transform.position,gameObject.transform.position) < 
                Vector3.Distance(enemyCollider.transform.position,gameObject.transform.position)))
            {
                enemyCollider = enemy;
            }
        }
        if (enemyCollider != null)
        {
            enemyCollider.GetComponent<Enemy>().TakeDamage(damage);
            enemyCollider.GetComponent<Enemy>().Stun(stunDuration);
        }

            cooldownTimer = cooldown;
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
