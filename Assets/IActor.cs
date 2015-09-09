namespace Assets
{
    public interface IActor
    {
        ITargetable Targetable { get; }
        IAttackable Attackable { get; }
    }
}