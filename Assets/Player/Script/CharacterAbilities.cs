using Assets.Player.Script.Abilities;
using System;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    public event EventHandler OnActivated;
    [SerializeField] private IAbility ability1;
    [SerializeField] private IAbility ability2;
    [SerializeField] private IAbility ability3;
    private void Awake()
    {
        ability1 = GetComponent<FearAbility>();
        ability2 = GetComponent<DashAbility>();
        ability3 = GetComponent<TerrifyingScreamAbility>();

        ability1.OnActivated += AbilityActivate;
        ability2.OnActivated += AbilityActivate;
        ability3.OnActivated += AbilityActivate;
    }
    public void OnAbility1()
    {
        ability1?.Activate();
    }

    public void OnAbility2()
    {
        ability2.Activate();
    }

    public void OnAbility3()
    {
        ability3?.Activate();
    }
    private void AbilityActivate(object sender, EventArgs e)
    {
        OnActivated?.Invoke(sender, e);
    }
}
