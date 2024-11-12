using Assets.Character;
using Assets.Player.Script.Abilities;
using Assets.UI.Dialogue;
using System;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    public event EventHandler OnActivated;
    [SerializeField] private IAbility ability1;
    [SerializeField] private IAbility ability2;
    [SerializeField] private IAbility ability3;
    private bool activeAbility1 = false;
    private bool activeAbility2 = false;
    private bool activeAbility3 = false;
    private DialogueSender dialogSender;
    private bool isSending3 = false;
    private void Awake()
    {
        ability1 = GetComponent<FearAbility>();
        ability2 = GetComponent<DashAbility>();
        ability3 = GetComponent<TerrifyingScreamAbility>();

        ability1.OnActivated += AbilityActivate;
        ability2.OnActivated += AbilityActivate;
        ability3.OnActivated += AbilityActivate;
        dialogSender = GetComponent<DialogueSender>();
    }
    public void OnAbility1()
    {
        if(activeAbility1)
        ability1?.Activate();
    }

    public void OnAbility2()
    {
        if(activeAbility2)
        ability2.Activate();
    }

    public void OnAbility3()
    {
        if(activeAbility3)
        ability3?.Activate();
    }
    public void AbilityActivate(object sender, EventArgs e)
    {
        if(e is EventOpenAbilities op)
        {
            activeAbility1 = op.Ability1;
            activeAbility2 = op.Ability2;
            activeAbility3 = op.Ability3;
            if (activeAbility2 && !isSending3)
            {
                dialogSender.SendMessage(2);
                isSending3 = true;
            }
        }
    }
}
