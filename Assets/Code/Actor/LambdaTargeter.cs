using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class LambdaTargeter : ITargeter
    {
        private readonly Func<IEnumerable<ITargetable>> _getTargets;
        private readonly Func<Vector3> _getPosition;

        public LambdaTargeter(Func<IEnumerable<ITargetable>> getTargets, Func<Vector3> getPosition)
        {
            _getTargets = getTargets;
            _getPosition = getPosition;
        }

        public void Target(ITargetable target)
        {
            CurrentTarget = target;
        }

        public bool HasTarget
        {
            get
            {
                if (CurrentTarget != null && !CurrentTarget.IsValid)
                    CurrentTarget = null;
                return CurrentTarget != null;
            }
        }

        public ITargetable CurrentTarget { get; private set; }
        public Vector3 Position { get { return _getPosition(); } }

        public void CycleNextTarget()
        {
            var targets = _getTargets().OrderBy(a => Vector3.Distance(_getPosition(), a.Position));
            if (!targets.Any())
                return;
            var currentPosition = _getPosition();
            if (HasTarget)
            {
                var currentDistance = Vector3.Distance(currentPosition, CurrentTarget.Position);
                CurrentTarget =
                    targets.FirstOrDefault(a => Vector3.Distance(a.Position, currentPosition) >= currentDistance && a != CurrentTarget) ??
                    targets.First();
            }
            else
            {
                CurrentTarget = targets.First();
            }
        }
    }
}