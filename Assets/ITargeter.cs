using UnityEngine;

namespace Assets
{
    public interface ITargeter
    {
        void Target(ITargetable target);
        bool HasTarget { get; }
        ITargetable CurrentTarget { get; }
        /// <summary>
        /// Sets the CurrentTarget to the next available target
        /// </summary>
        void CycleNextTarget();
    }
}