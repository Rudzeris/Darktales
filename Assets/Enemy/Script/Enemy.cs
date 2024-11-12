using Assets.Character;
using Assets.UI.Dialogue;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private int _hp = 5;
    [SerializeField] private static int _max_hp = 5;
    [SerializeField] private bool _dead = false;
    [SerializeField] private float _get_damage_cooldown = 0.3f;
    [SerializeField] private float _get_damage_time = 0f;
    [SerializeField] private bool _isDamage;
    private Vector3 _spawnPoint;
    private DialogTrigger dialogTrigger = new DialogTrigger();
    public int HP
    {
        get => _hp;
        set
        {
            _hp = (value < _hp ? value : (value > _max_hp ? _max_hp : value));
            UpdateState();
        }
    }
    public bool IsDamage => _isDamage;
    public bool IsDead => _dead;
    private LevelManager levelManager;
    public event EventHandler OnDie;
    public event EventHandler OnHit;
    
    private EnemyController2D enemyController;
    private EnemyAttack enemyAttack;

    // Новые поля для оглушения
    private bool isStunned = false;
    private float stunTimer = 0f;

    private void Awake()
    {
        dialogTrigger.IsOneTriger = true;
        _spawnPoint = gameObject.transform.position;
        enemyController = gameObject.GetComponent<EnemyController2D>();
        enemyAttack = gameObject.GetComponent<EnemyAttack>();
    }

    private void FixedUpdate()
    {
        if (_isDamage)
        {
            if (_get_damage_time <= 0)
            {
                _isDamage = false;
            }
            else
            {
                _get_damage_time -= Time.fixedDeltaTime;
            }
        }

        // Обработка оглушения
        if (isStunned)
        {
            stunTimer -= Time.fixedDeltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                ResumeEnemyBehavior();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!_isDamage)
        {
            HP -= (damage > 0 ? damage : 0);
            _get_damage_time = _get_damage_cooldown;
            _isDamage = true;
        }
    }

    private void UpdateState()
    {
        OnHit?.Invoke(this, EventArgs.Empty);
        if (_hp <= 0)
        {
            _hp = 0;
            OnDie?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _dead = false;
    }

    private void OnDisable()
    {
        _dead = true;
    }

    // Реализация метода Stun
    public void Stun(float duration)
    {
        gameObject.GetComponent<DialogueSender>().SendMessage(0);
        isStunned = true;
        stunTimer = duration;
        StopEnemyBehavior();
    }

    private void StopEnemyBehavior()
    {
        enemyController.enabled = false;
        enemyAttack.enabled = false;
    }

    private void ResumeEnemyBehavior()
    {
        enemyController.enabled = true;
        enemyAttack.enabled = true;
    }
}
