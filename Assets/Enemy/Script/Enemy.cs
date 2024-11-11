using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _score = 10;
    [SerializeField] private int _hp = 5;
    [SerializeField] private static int _max_hp = 5;
    [SerializeField] private bool _dead = false;
    [SerializeField] private float _get_damage_cooldown = 0.3f;
    [SerializeField] private float _get_damage_time = 0f;
    [SerializeField] private bool _isDamage;
    private Vector3 _spawnPoint;
    public int Score
    {
        get => _score;
        set => _score = value;
    }
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
    public delegate void DieHandler(Enemy enemy);
    public delegate void HitHandler(Enemy enemy);
    public event DieHandler OnDie;
    public event HitHandler OnHit;

    private void Awake()
    {
        _spawnPoint = gameObject.transform.position;
    }
    private void FixedUpdate()
    {
        if (_isDamage)
        {
            if (_get_damage_time<=0)
            {
                _isDamage = false;
            }
            else
            {
                _get_damage_time -= Time.fixedDeltaTime;
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

        OnHit?.Invoke(this);
        if (_hp <= 0)
        {
            _hp = 0;
            OnDie?.Invoke(this);
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
}
