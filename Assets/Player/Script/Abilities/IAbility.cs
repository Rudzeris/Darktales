namespace Assets.Player.Script.Abilities
{
    public interface IAbility
    {
        void Activate();
        float Cooldown { get; }
    }

}
