using Assets.Level.Objects;
using System;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackVerticalForce = 0.3f;
    private Collider2D attackPointCollider;
    private void Awake()
    {
        attackPointCollider = GetComponent<CapsuleCollider2D>();
    }
    public AudioSource DamageToEnemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemy.IsDamage)
            {
                if (DamageToEnemy != null)
                    DamageToEnemy.Play();
                // Наносим урон врагу
                enemy.TakeDamage(damage);
                

                // Отталкиваем врага
                Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 knockbackDirection = other.transform.position - transform.position;
                    knockbackDirection.Normalize();
                    knockbackDirection.y += knockbackVerticalForce; // Добавляем вертикальную составляющую
                    enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
         else if (other.CompareTag("ContactObject"))
        {
            IContactObject obj = other.GetComponent<IContactObject>();
            obj?.Contact(this, EventArgs.Empty);
        }

    }


}