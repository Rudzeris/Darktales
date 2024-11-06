using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackVerticalForce = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // ������� ���� �����
            other.GetComponent<Enemy>().TakeDamage(damage);

            // ����������� �����
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = other.transform.position - transform.position;
                knockbackDirection.Normalize();
                knockbackDirection.y += knockbackVerticalForce; // ��������� ������������ ������������
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }


}