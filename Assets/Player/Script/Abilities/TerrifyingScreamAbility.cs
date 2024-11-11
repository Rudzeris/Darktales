using Assets.Character;
using System;
using UnityEngine;

namespace Assets.Player.Script.Abilities
{
    public class TerrifyingScreamAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private float stunDuration = 5f;
        [SerializeField] private float radius = Screen.width / 4;
        [SerializeField] private float cooldown = 30f;
        [SerializeField] private int toSpendSaturation = 0;

        [SerializeField] private float cooldownTimer = 0f;

        public float Cooldown { get { return cooldown; } }

        public event EventHandler OnActivated;

        public void Activate()
        {
            if (cooldownTimer > 0)
            {
                Debug.Log($"Ability is on cooldown. Wait {cooldownTimer} sec");
                return;
            }

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                    enemy.GetComponent<Enemy>().Stun(stunDuration);
                }
            }
            OnActivated?.Invoke(this, new EventSaturation(toSpendSaturation));

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
}
