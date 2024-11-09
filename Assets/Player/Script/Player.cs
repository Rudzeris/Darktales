using Assets.Character;
using System;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    public event EventHandler OnDie;
    public event EventHandler OnHit;

    private LevelManager LevelManager;

    private void Awake()
    {
        LevelManager = FindObjectOfType<LevelManager>();
    }

    public void TakeDamage(int damage)
    {
        OnHit(this, new EventDamage(damage));
        
        if (LevelManager is not null && LevelManager.Saturation == 0)
        {
            OnDie(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
    }
}
