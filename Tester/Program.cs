using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets;
using UnityEngine;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var targets = new List<ITargetable>();

            for (var i = 0; i < 5; i++)
            {
                targets.Add(new SimpleTargetable(new Vector3(1, 1, 0) * i, null));
            }

            var targeter = new LambdaTargeter(() => targets, () => Vector3.zero);

            for (int i = 0; i < 20; i++)
            {
                targeter.CycleNextTarget();
                Console.WriteLine(Vector3.Distance(targeter.CurrentTarget.Position, targeter.Position));
            }
            Console.ReadKey();

        }
    }
}
