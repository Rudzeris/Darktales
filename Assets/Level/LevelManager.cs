using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// Класс, который знает информацию про всех и добавляем им event 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelHUD levelHUD;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private int[] LVLs = new int[3] { 50, 75, 100 };
    private LevelProgress progress;

    [SerializeField] public int Saturation
    {
        get => progress.Saturation;
        set { progress.Saturation = value; levelHUD.UpdateSaturation(value); }
    }

    private void Awake()
    {
        progress = new LevelProgress();
        progress.Saturation = 76;
        enemies = FindObjectsOfType<Enemy>().ToList();
        foreach (Enemy enemy in enemies)
        {
            enemy.OnDie += EnemyDie;
        }
        levelHUD.LVLs = LVLs;
    }
    public void Start()
    {
        levelHUD.UpdateSaturation(Saturation);
    }
    private void EnemyDie(Enemy enemy)
    {
        if (!enemy.IsDead)
        {
            Saturation += enemy.Score;
        }
        
    }
}
