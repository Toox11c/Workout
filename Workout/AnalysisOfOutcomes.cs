using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout
{
    public abstract class ProbabilityCalculator
    {
        private List<Race> racesHistory;

        protected ProbabilityCalculator(List<Race> racesHistory)
        {
            this.racesHistory = racesHistory;
        }

        public abstract double CalculatePlaceProbability(int runnerId, int place);
        public abstract double CalculateTopNProbability(int runnerId, int n);
        public abstract double CalculatePairProbability(int runnerX, int runnerY, int placeX, int placeY);

    }
    public class BasicProbabilityCalculator : ProbabilityCalculator
    {
        private List<Race> AllRace;
        public BasicProbabilityCalculator(List<Race> racesHistory) : base (racesHistory)
        {
            AllRace = racesHistory;
        }

        public override double CalculatePlaceProbability(int runnerId, int place)
        {
            var count = AllRace.Count (race =>
            race.Results.Any (r=> r.Runner.Id == runnerId && r.Place <= place));
            return (double) count / AllRace.Count;
        }
        public override double CalculateTopNProbability(int runnerId, int n)
        {
            var count = AllRace.Count(race =>
            race.Results.Any(r => r.Runner.Id == runnerId && r.Place == n));
            return (double)count / AllRace.Count;
        }
        public override double CalculatePairProbability(int runnerX, int runnerY, int placeX, int placeY)
        {
            int count = AllRace.Count(race =>
            {
                var resultX = race.Results.FirstOrDefault(r => r.Runner.Id == runnerX);
                var resultY = race.Results.FirstOrDefault(r => r.Runner.Id == runnerY);
                return resultX != null && resultY != null&&
                resultX.Place == placeX && resultY.Place == placeY;
            });
            return (double)count / AllRace.Count;
        }
    }
}

