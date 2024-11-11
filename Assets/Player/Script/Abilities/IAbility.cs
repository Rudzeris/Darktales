using System;
using UnityEngine.EventSystems;

namespace Assets.Player.Script.Abilities
{
    public interface IAbility
    {
        event EventHandler OnActivated;
        void Activate();
        float Cooldown { get; }
    }

}
