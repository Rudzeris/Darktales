using Assets.Player.Script.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilities : MonoBehaviour
{
    [SerializeField] private IAbility ability1;
    [SerializeField] private IAbility ability2;
    [SerializeField] private IAbility ability3;
    private void Awake()
    {
        ability1 = GetComponent<FearAbility>();
        ability2 = GetComponent<DashAbility>();
        ability3 = GetComponent<TerrifyingScreamAbility>();
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
}
