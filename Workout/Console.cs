using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Workout;

namespace Console
{
    class Simulation : runner
    {
        public Runner Runner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        static void Main()
        {
        var runner = GetRunnerFromUser();
        int distance = GetDistanceFromUser();

        }


    }
}
