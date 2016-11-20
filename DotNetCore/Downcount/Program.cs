using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DownCount.Entities;
using DownCount.Engine;
using DownCount.Entities.Equations;

namespace DownCount.ConsoleTester
{
    /// <summary>
    /// A simple console app that allows you to play downcount
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to DownCount");

            PlayGame();

            Console.WriteLine("Do you wish to play again, Y/N");
            ConsoleKeyInfo key = Console.ReadKey();
            while (key.KeyChar == 'y')
            {
                Console.WriteLine();
                PlayGame();
                Console.WriteLine("Do you wish to play again, Y/N");
                key = Console.ReadKey();
            }
        }

        private static void PlayGame()
        {
            Console.WriteLine("How many big numbers would you like, please enter a number between 1 and 6");
            ConsoleKeyInfo input = Console.ReadKey();
            int numBigNumbers;
            string keyValue = input.KeyChar.ToString();
            while (!int.TryParse(keyValue, out numBigNumbers) ||
                numBigNumbers < 1 ||
                numBigNumbers > 6)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid number entered");
                Console.WriteLine("Please enter a number between 1 and 6");
                input = Console.ReadKey();
                keyValue = input.KeyChar.ToString();
            }

            // Calculate the numbers used in this game
            List<int> numbers = new List<int>();

            for (int i = 0; i < numBigNumbers; ++i)
            {
                numbers.Add(RandomNumberHelper.RandomIntegerFromSet(25, 50, 75, 100));
            }

            for (int i = 0; i < 6 - numBigNumbers; ++i)
            {
                numbers.Add(RandomNumberHelper.RandomInteger(1, 10));
            }
            IntegerSet sourceNumbers = new IntegerSet(numbers);

            int targetNumber = RandomNumberHelper.RandomInteger(101, 999);

            Console.WriteLine();
            Console.WriteLine("The game is to find {0} from the set of numbers {1}, you have 30 seconds starting from now", targetNumber, sourceNumbers.ToString());
            Console.WriteLine();

            // Show Timer
            Task timer = Task.Factory.StartNew(() => TimerCountDown(30));

            // Calculate Answer
            DownCountGame game = new DownCountGame(targetNumber, sourceNumbers);
            Task<Solutions> calculationTask = CalculateSoultionASync(game);

            timer.Wait();

            // Display Result
            if (calculationTask.IsCompleted)
            {
                try
                {
                    Solutions solutions = calculationTask.Result;
                    if (solutions.Count == 0)
                    {
                        Console.WriteLine("Downcount could not find a solution");
                    }
                    else
                    {
                        IEquation solution = solutions[0];
                        if (solution.Value == game.TargetNumber)
                        {
                            Console.WriteLine("Downcount found an exact solution");
                            Console.WriteLine("{0} = {1}", targetNumber, solution.ToString());
                        }
                        else
                        {
                            decimal closeValue = solution.Value;
                            Console.WriteLine("Downcount found a solution within {0} of the original target number", game.TargetNumber - closeValue);
                            Console.WriteLine("{0} = {1}", solution.Value, solution.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("An error occured calculating the solution: {0}", ex.ToString()));
                }
            }
            else
            {
                // We ran out of time
                Console.WriteLine("Downcount could not find a solution in the time given");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Calculate the solution to the game
        /// </summary>
        private static Task<Solutions> CalculateSoultionASync(DownCountGame game)
        {
            Task<Solutions> calculationTask = new Task<Solutions>(() => CalculateSolution(game));//, cancellSource.Token);
            calculationTask.Start();
            return calculationTask;
        }

        /// <summary>
        /// Calculate the solution to the game
        /// </summary>
        private static Solutions CalculateSolution(DownCountGame game)
        {
            Solutions solutions = DownCountSolver.Solve(game, false);
            if (solutions.Count == 0)
            {
                //Look for a 'close' soltion
                DownCountGame closeGame = new DownCountGame(game.TargetNumber, game.Numbers.ToArray()); ;
                for (int i = 1; i <= 10; ++i)
                {
                    closeGame.TargetNumber = game.TargetNumber - i;
                    solutions = DownCountSolver.Solve(closeGame, false);
                    if (solutions.Count > 0)
                        return solutions;

                    closeGame.TargetNumber = game.TargetNumber + i;
                    solutions = DownCountSolver.Solve(closeGame, false);
                    if (solutions.Count > 0)
                        return solutions;
                }
            }

            return solutions;
        }

        private static void TimerCountDown(int seconds)
        {
            for (int i = seconds; i >= 0; --i)
            {
                Thread.Sleep(1000);
                Console.Write("\r{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Return an integer set from the input string (comma seperated list of integers)
        /// </summary>
        /// <param name="input"></param>
        /// <returns>null is input string is invalid</returns>
        private static IntegerSet GetNumbers(string input)
        {
            IntegerSet set = new IntegerSet();
            string[] numbers = input.Split(',');
            if (numbers.Length == 0)
                return null;

            foreach (string c in numbers)
            {
                int result;
                if (int.TryParse(c.Trim(), out result))
                    set.Add(result);
                else
                    return null;
            }

            return set;
        }
    }
}
