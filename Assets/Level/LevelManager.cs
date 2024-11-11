using Assets.Character;
using Assets.Level.Objects;
using Assets.Level.Objects.Door;
using Assets.Player.Script.Abilities;
using Assets.UI.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// Класс, который знает информацию про всех и добавляем им event 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelHUD levelHUD;
    [SerializeField] private int[] Lvls = new int[5] { 0, 50, 75, 100, 125 };
    [SerializeField] private int startSaturation = 33;
    [SerializeField] private int getSaturation = 10;
    [SerializeField] private int lostSaturation = 3;
    private List<Enemy> enemies;
    private List<IContactObject> contactObjects = new List<IContactObject>();
    private Player player;
    public event EventHandler OnOpenAbilities;
    private LevelProgress progress;
    private DialogueMessage message;

    [SerializeField]
    public int Saturation
    {
        get => progress.Saturation;
        set
        {
            if (value < Saturation)
                progress.RemoveSaturation(Saturation - value);
            else
                progress.AddSaturation(value - Saturation);
            OpenAbilities();
            levelHUD?.UpdateSaturation(Saturation);
        }
    }
    public void LoadProgress(LevelProgress progress) => this.progress = progress;
    private void Awake()
    {
        Time.timeScale = 1;
        progress = new LevelProgress();
        Saturation = startSaturation;
        // Enemy
        enemies = FindObjectsOfType<Enemy>().ToList();
        foreach (Enemy enemy in enemies)
        {
            enemy.OnDie += EnemyDie;
        }
        // LevelHUD
        //levelHUD = GetComponent<LevelHUD>();
        if (levelHUD != null)
        {
            levelHUD.Lvls = Lvls;
            OnOpenAbilities += levelHUD.ActivateAbilities;
        }
        // Player
        if (player is null)
            player = FindObjectOfType<Player>();

        if (player is not null)
        {
            player.OnDie += PlayerDie;
            player.OnHit += PlayerHit;
        }
        // Door
        foreach (var i in FindObjectsOfType<Door>())
        {
            contactObjects.Add(i);
            i.OnContact += ContactObject;
        }
        // Abilities
        var abilities = player.GetComponent<CharacterAbilities>();
        abilities.OnActivated += (object sender, EventArgs e) =>
        {
            if (sender is IAbility && e is EventSaturation sat)
                Saturation -= sat.Saturation;
        };
        OnOpenAbilities += abilities.AbilityActivate;
        Saturation += 0;
        
    }
    public void Start()
    {
        levelHUD?.UpdateSaturation(Saturation);
        // Message
        if (levelHUD != null)
        {
            message = FindObjectOfType<DialogueMessage>();
            foreach (var sender in FindObjectsOfType<DialogueSender>())
            {
                if (message != null && sender != null)
                {
                    sender.OnSendMessage += message.ShowMessage;
                    Debug.Log("Subscribed to OnSendMessage");
                }
                else
                {
                    Debug.Log("DialogueSender or DialogueMessage is null.");
                }
            }

        }
    }
    // Enemy
    private void EnemyDie(object sender, EventArgs args)
    {
        if (sender is Enemy enemy && !enemy.IsDead)
            Saturation += getSaturation;
    }
    // Player
    private void PlayerDie(object sender, EventArgs args)
    {
        if (sender is Player player)
        {
            RestartScene();
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
        OnOpenAbilities?.Invoke(this, new EventOpenAbilities(
            Saturation >= Lvls[0],
            Saturation >= Lvls[1],
            Saturation >= Lvls[2]
            ));
    }
    // Contact
    private void ContactObject(object sender, EventArgs args)
    {
        switch (sender)
        {
            case Door door:
                ContactDoor(door, args);
                break;
        }
    }
    private void ContactDoor(Door door, EventArgs args)
    {
        if (args is EventDoor eventDoor)
        {
            switch (eventDoor.PreviousState)
            {
                case DoorState.Open:
                    if (eventDoor.CurrentState == DoorState.Closed)
                        progress.AddClosedDoor();
                    break;
                case DoorState.Closed:
                    if (eventDoor.CurrentState == DoorState.Open)
                        progress.AddOpenDoor();
                    break;
                case DoorState.Locked:
                    if (eventDoor.CurrentState != DoorState.Locked)
                        progress.AddUnlockedDoor();
                    break;
            }
            Debug.Log($"Door: {eventDoor.CurrentState}");
        }
    }
    private void RestartScene()
    {
        UnityEngine.SceneManagement.Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

}
