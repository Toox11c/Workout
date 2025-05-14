using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout
{
    public class Runner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ReactionTime { get; set; } 
        public double MaxSpeed { get; set; }     
    }
    public class RaceResult
    {
        internal int runnerId;

        public int Place { get; set; }
        public double Time { get; set; }
        public Runner Runner { get; internal set; }
        public object Runners { get; internal set; }
    }
    public class Race
    {
        public List<RaceResult> Results { get; set; }
        public int Distance { get; internal set; }
    }

    public class RaceGenerator
    {

        public Race SimulateRace(List<Runner> runners, int distance )
        {
            var results = runners.Select(r =>
            {
                double time = CalculateRunTime(r, distance);
                return new RaceResult
                {
                    Runner = r,
                    Time = time,
                    Place = 0 
                };
            })
            .OrderBy(r => r.Time)
            .ToList();
            for (int i = 0; i < results.Count; i++)
            {
                results[i].Place = i + 1;
            }
            return new Race
            {
                Distance = distance,
                Results = results
            };
        }

        private double CalculateRunTime(Runner runner, int distance)
        {
            return runner.ReactionTime + (distance / runner.MaxSpeed);
        }
    }
}
