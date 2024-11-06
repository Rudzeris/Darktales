using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// Класс, который знает информацию про всех и добавляем им event 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelHUD levelHUD;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private uint[] LVLs = new uint[3] { 50, 75, 100 };
    private LevelProgress progress;

    [SerializeField] public int Saturation
    {
        get => progress.Saturation;
        set => progress.Saturation = value;
    }
    public int LvlSaturation
    {
        get
        {
            int lvl = 0;
            foreach (int i in LVLs)
                if (Saturation >= i) lvl++;
            return lvl;
        }
    }

    private void Awake()
    {
        progress = new LevelProgress();
        enemies = FindObjectsOfType<Enemy>().ToList();
        foreach (Enemy enemy in enemies)
        {
            enemy.OnDie += EnemyDie;
            enemy.OnHit += EnemyGetDamage;
        }
    }

    private void EnemyGetDamage(Enemy enemy)
    {
        if (!enemy.IsDead)
        {
            
        }
    }

    private void EnemyDie(Enemy enemy)
    {
        if(!enemy.IsDead)
            Saturation += enemy.Score;
    }
    

}
