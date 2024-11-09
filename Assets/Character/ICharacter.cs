using System;

namespace Assets.Character
{
    public interface ICharacter
    {
        event EventHandler OnDie;
        event EventHandler OnHit;
        void TakeDamage(int damage);
    }
}
