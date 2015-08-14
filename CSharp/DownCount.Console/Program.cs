using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DownCount.Entities;
using DownCount.Engine;

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
            Console.WriteLine("The game is to find {0} from the set of numbers {1}", targetNumber, numbers.ToString());
            Console.WriteLine();

            // Display the solutions
            Solutions solutions = DownCountSolver.Solve(false, targetNumber, numbers.ToArray());
            if (solutions.Count == 0)
            {
                Console.WriteLine("No exact solutions found");
 
                //Look for a 'close' soltion
                for (int i = 1; i <= 10; ++i)
                {
                    int closeTargetNumber = targetNumber - i;
                    solutions = DownCountSolver.Solve(false, closeTargetNumber, numbers.ToArray());
                    if (solutions.Count > 0)
                    {
                        Console.WriteLine("Solution found within {0} of the original target number", i.ToString());
                        Console.WriteLine("{0} = {1}", closeTargetNumber, solutions[0].ToString());
                        break;
                    }

                    closeTargetNumber = targetNumber +i;
                    solutions = DownCountSolver.Solve(false, closeTargetNumber, numbers.ToArray());
                    if (solutions.Count > 0)
                    {
                        Console.WriteLine("Solution found within {0} of the original target number", i.ToString());
                        Console.WriteLine("{0} = {1}", closeTargetNumber, solutions[0].ToString());
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("An exact solution was found");
                Console.WriteLine("{0} = {1}", targetNumber, solutions[0].ToString());
            }

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
