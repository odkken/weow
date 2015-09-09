using UnityEngine;

namespace Assets
{
    public interface ITargetable
    {
        Vector3 Position { get; }
        bool IsValid { get; }
        IAttackable Attackable { get; }
    }
}
