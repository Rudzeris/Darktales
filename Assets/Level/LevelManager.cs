using Assets.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Класс, который знает информацию про всех и добавляем им event 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelHUD levelHUD;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private Player player;
    [SerializeField] private int[] Lvls = new int[5] { 0, 50, 75, 100, 125 };
    [SerializeField] private int startSaturation = 33;
    [SerializeField] private int getSaturation = 10;
    [SerializeField] private int lostSaturation = 3;
    public event EventHandler OnOpenAbilities;
    private LevelProgress progress;

    [SerializeField]
    public int Saturation
    {
        get => progress.Saturation;
        set { 
            progress.Saturation = value; 
            levelHUD?.UpdateSaturation(value); 
            OpenAbilities(); }
    }

    private void Awake()
    {
        levelHUD = GetComponent<LevelHUD>();
        progress = new LevelProgress();
        Saturation = startSaturation;
        enemies = FindObjectsOfType<Enemy>().ToList();
        foreach (Enemy enemy in enemies)
        {
            enemy.OnDie += EnemyDie;
        }
        if (levelHUD != null)
            levelHUD.Lvls = Lvls;
        if (player is not null)
        {
            player = FindObjectOfType<Player>();
            player.OnDie += PlayerDie;
            player.OnHit += PlayerHit;
        }
    }
    public void Start()
    {
        levelHUD?.UpdateSaturation(Saturation);
    }
    private void EnemyDie(object sender, EventArgs args)
    {
        if (sender is Enemy enemy && !enemy.IsDead)
            Saturation += getSaturation;
    }
    private void PlayerDie(object sender, EventArgs args)
    {
        if (sender is Player player)
        {

        }
    }
    private void PlayerHit(object sender, EventArgs args)
    {
        if (sender is Player)
            if (args is EventDamage eventDamage)
                Saturation -= eventDamage.Damage;
            else
                Saturation -= lostSaturation;
    }
    private void OpenAbilities()
    {
        OnOpenAbilities(this, new EventOpenAbilities(
            Saturation >= Lvls[1],
            Saturation >= Lvls[2],
            Saturation >= Lvls[3]
            ));
    }
}
