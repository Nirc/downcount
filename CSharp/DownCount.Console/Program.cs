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
            // Read in a list of integers for the game
            Console.WriteLine("Enter a comma seperated list of integers, e.g. 3,5,67,8 and press enter");
            string input = Console.ReadLine();
            IntegerSet numbers = GetNumbers(input);
            while (numbers == null)
            {
                Console.WriteLine("Invalid numbers eneterd");
                Console.WriteLine("Enter a comma seperated list of integers, e.g. 3,5,67,8 , and press enter");
                input = Console.ReadLine();
                numbers = GetNumbers(input);
            }

            // Read in a targer number
            Console.WriteLine("Enter a target integer and press enter");
            input = Console.ReadLine();
            int targetNumber;
            while (!int.TryParse(input.Trim(), out targetNumber))
            {
                Console.WriteLine("Invalid number enterd");
                Console.WriteLine("Enter a target integer and press enter");
                input = Console.ReadLine();
            }

            Console.WriteLine();
            Console.WriteLine("The game is to find {0} from the set of numbers {1}, you have 30 seconds starting from now", targetNumber, numbers.ToString());
            Console.WriteLine();

            // Show Timer
            Task timer = Task.Factory.StartNew(() => TimerCountDown(30));            

            // Calculate Answer
            DownCountGame game = new DownCountGame(targetNumber, numbers);
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
