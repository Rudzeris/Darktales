using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _score = 10; 
    [SerializeField] private int _hp = 5;
    [SerializeField] private static int _max_hp = 100;
    [SerializeField] private bool _dead = false;
    public int Score {
        get => _score;
        set => _score = value;
    }
    public int HP {
        get => _hp;
        set { 
            _hp = (value<_hp?value:(value>_max_hp?_max_hp:value));
            UpdateState();
        }
    }
    public bool IsDead => _dead;
    private LevelManager levelManager;
    public delegate void DieHandler(Enemy enemy);
    public delegate void HitHandler(Enemy enemy);
    public event DieHandler OnDie;
    public event HitHandler OnHit;
    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
    private void UpdateState()
    {

        OnHit?.Invoke(this);
        if(_hp <= 0)
        {
            _hp = 0;
            OnDie?.Invoke(this);
            _dead = true;
        }
    }
}
