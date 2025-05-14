using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Workout;

namespace Workout
{ 

    class usage
    {
        static int GetIntInput(string prompt, int min, int max)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                    return value;
                Console.WriteLine($"Ошибка! Введите число от {min} до {max}:");
            }
        }
        static List<Runner> GetRunnerFromUser()
        {
            var runners = new List<Runner>();
            Console.WriteLine("Введите количество бегунов:");
            int runnerCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < runnerCount; i++)
            {
                Console.WriteLine($"\nБегун #{i + 1}");

                Console.Write("Имя: ");
                string name = Console.ReadLine();

                Console.Write("Время реакции (0.1-0.3 сек): ");
                double reactionTime = Convert.ToDouble(Console.ReadLine());

                Console.Write("Максимальная скорость (м/с): ");
                double maxSpeed = Convert.ToDouble(Console.ReadLine());

                runners.Add(new Runner
                {
                    Id = i + 1,
                    Name = name,
                    ReactionTime = reactionTime,
                    MaxSpeed = maxSpeed
                });
            }

            return runners;
        }
        static void CalculatePlaceProbability(BasicProbabilityCalculator analiz, List<Runner> runners)
        {
            Console.WriteLine("Доступные бегуны:");
            foreach (var runner in runners)
            {
                Console.WriteLine($"{runner.Id}. {runner.Name}");
            }

            Console.Write("Введите ID бегуна: ");
            int runnerId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите место (1-6): ");
            int place = Convert.ToInt32(Console.ReadLine());

            double probability = analiz.CalculatePlaceProbability(runnerId, place);
            Console.WriteLine($"Вероятность: {probability:P2}");
        }
        static void CalculateTopNProbability(BasicProbabilityCalculator analiz, List<Runner> runners)
        {
            Console.WriteLine("Доступные бегуны:");
            foreach (var runner in runners)
            {
                Console.WriteLine($"{runner.Id}. {runner.Name}");
            }
            Console.Write("Введите ID бегуна: ");
            int runnerId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите место (1-6): ");
            int n = Convert.ToInt32(Console.ReadLine());

            double probability = analiz.CalculateTopNProbability(runnerId, n);
            Console.WriteLine($"Вероятность: {probability:P2}");
        }
        static void CalculatePairProbability(BasicProbabilityCalculator analiz, List<Runner> runners)
        {
            
                Console.WriteLine("Доступные бегуны:");
                foreach (var runner in runners)
                {
                    Console.WriteLine($"{runner.Id}. {runner.Name}");
                }

                Console.WriteLine("\nПервый бегун:");
                Console.Write("Введите ID первого бегуна: ");
                int runnerX = GetIntInput("", 1, runners.Count);

                Console.Write("Введите желаемое место первого бегуна (1-6): ");
                int placeX = GetIntInput("", 1, 6);

                Console.WriteLine("\nВторой бегун:");
                Console.Write("Введите ID второго бегуна: ");
                int runnerY = GetIntInput("", 1, runners.Count);

                Console.Write("Введите желаемое место второго бегуна (1-6): ");
                int placeY = GetIntInput("", 1, 6);

                double probability = analiz.CalculatePairProbability(runnerX, runnerY, placeX, placeY);
                Console.WriteLine($"\nВероятность того, что {runners.First(r => r.Id == runnerX).Name} будет {placeX}-м " +
                                 $"и {runners.First(r => r.Id == runnerY).Name} будет {placeY}-м: {probability:P2}\n");
            
        }


        static void Main(string[] args)
        {
            var runner = GetRunnerFromUser();
            Console.WriteLine("введите дистанцию в метрах: ");
            int distance = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("введите колличество забегов: ");
            int races = Convert.ToInt32(Console.ReadLine());

            var generator = new RaceGenerator();
            var racesHistory = new List<Race>();
            for (int i = 0; i < races; i++)
            {
                racesHistory.Add(generator.SimulateRace(runner, distance));
            }
            var analiz = new BasicProbabilityCalculator(racesHistory);
            while (true)
            {
                Console.WriteLine("\n выберите действие:");
                Console.WriteLine("1. Вероятность места для бегуна");
                Console.WriteLine("2. Вероятность попадания в топ-N");
                Console.WriteLine("3. Парная вероятность");
                Console.WriteLine("4. Выход");
                var vabor = Console.ReadLine();

                switch (vabor)
                {
                    case "1":
                        CalculatePlaceProbability(analiz, runner);
                        break;
                    case "2":
                        CalculateTopNProbability(analiz, runner);
                        break;
                    case "3":
                        CalculatePairProbability (analiz, runner);
                        break;
                    case "4":
                        return;
                        default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }

    }

}
 