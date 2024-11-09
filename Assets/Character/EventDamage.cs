using System;

namespace Assets.Character
{
    public class EventDamage : EventArgs
    {
        public int Damage { get; private set; }
        public EventDamage(int damage)
        {
            Damage = damage;
        }
    }
}
