using System;

namespace Assets.Character
{
    public class EventOpenAbilities : EventArgs
    {
        public bool Ability1 { get; private set; }
        public bool Ability2 { get; private set; }
        public bool Ability3 { get; private set; }
        public EventOpenAbilities(bool ability1, bool ability2, bool ability3)
        {
            Ability1 = ability1;
            Ability2 = ability2;
            Ability3 = ability3;
        }
    }
}
