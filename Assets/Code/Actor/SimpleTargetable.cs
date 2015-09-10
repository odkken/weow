using UnityEngine;

namespace Assets
{
    public class SimpleTargetable : ITargetable
    {
        public SimpleTargetable(Vector3 position, IAttackable attackable)
        {
            Position = position;
            Attackable = attackable;
            IsValid = true;
        }
        public Vector3 Position { get; set; }
        public bool IsValid { get; set; }
        public IAttackable Attackable { get; private set; }
    }
}