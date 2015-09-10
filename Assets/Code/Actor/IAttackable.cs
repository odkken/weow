namespace Assets
{
    public interface IAttackable
    {
        void Attack(Spell spell);
    }

    public enum Spell
    {
        FrostBolt,
        IceLance,
        FrostNova,
        FireBall,
    }
}