using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DownCount.Entities;
using DownCount.Entities.Equations;

namespace DownCount.Engine
{
    /// <summary>
    /// The calculation engine that solves a game of downcount. The algorithm uses recursion and brute force 
    /// to iterate through all possible arithmitic combinations of the numbers
    /// looking for all solutions e.g.
    /// The set of solutions to the game, y from (x1, x2, x3...xn), is equivalent to the set of solutions to
    /// set of soltions[(y - x1) from (x2, x3, ..xn)] ADD x1  UNION
    /// set of soltions[(y + x1) from (x2, x3, ..xn)] MINUS x1 UNION
    /// set of soltions[(y / x1) from (x2, x3, ..xn)] MULTIPLY x1 (provided y/x1 is an integer) UNION
    /// set of soltions[(y * x1) from (x2, x3, ..xn)] DIVIDE x1 UNION
    /// etc etc etc recursivley 
    /// </summary>
    public class DownCountSolver
    {
        /// <summary>
        /// Solve the game targetNumber from numbers
        /// </summary>
        /// <param name="allSolutions"></param>
        /// <param name="targetNumber"></param>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static Solutions Solve(bool allSolutions, int targetNumber, params int[] numbers)
        {
            return Solve(new DownCountGame(targetNumber, numbers), allSolutions);
        }

        /// <summary>
        /// Solve the given downcount game
        /// </summary>
        /// <param name="game"></param>
        /// <param name="allSolutions"></param>
        /// <returns></returns>
        public static Solutions Solve(DownCountGame game, bool allSolutions = true)
        {
            Solutions solutions = new Solutions();

            if (game.Numbers.Contains(game.TargetNumber))
                solutions.Add(new Integer(game.TargetNumber));

            if (!allSolutions && solutions.Count > 0)
                return solutions;

            if (game.Numbers.Count == 1)
                return solutions;

            for (int i = 0; i < game.Numbers.Count; ++i)
            {
                solutions.AddRange(SolveSubset(game, i, allSolutions));

                if (!allSolutions && solutions.Count > 0)
                    return solutions;
            }

            return solutions;
        }

        /// <summary>
        /// Solve the game by solving the subset that considedrs the ith element in isolation
        /// </summary>
        /// <param name="game"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static Solutions SolveSubset(DownCountGame game, int i, bool allSolutions = true)
        {
            Solutions solutions = new Solutions();
            int currentNumber = game.Numbers[i];
            IntegerSet subset = game.Numbers.SubsetNotIncluding(i);
            DownCountGame gameAdd = new DownCountGame();
            gameAdd.TargetNumber = game.TargetNumber - currentNumber;
            gameAdd.Numbers = subset;
            solutions.AddRange(Solve(gameAdd, allSolutions) + new Integer(currentNumber));
            if (!allSolutions && solutions.Count > 0)
                return solutions;

            DownCountGame gameMinus = new DownCountGame();
            gameMinus.TargetNumber = game.TargetNumber + currentNumber;
            gameMinus.Numbers = subset;
            solutions.AddRange(Solve(gameMinus, allSolutions) - new Integer(currentNumber));
            if (!allSolutions && solutions.Count > 0)
                return solutions;

            DownCountGame gameMultiply = null;
            if (game.TargetNumber % currentNumber == 0)
            {
                gameMultiply = new DownCountGame();
                gameMultiply.TargetNumber = game.TargetNumber / currentNumber;
                gameMultiply.Numbers = subset;
                solutions.AddRange(Solve(gameMultiply, allSolutions) * new Integer(currentNumber));
                if (!allSolutions && solutions.Count > 0)
                    return solutions;
            }

            DownCountGame gameDivide = new DownCountGame();
            gameDivide.TargetNumber = game.TargetNumber * currentNumber;
            gameDivide.Numbers = subset;
            solutions.AddRange(Solve(gameDivide, allSolutions) / new Integer(currentNumber));
            if (!allSolutions && solutions.Count > 0)
                return solutions;

            return solutions;
        }


    }

}
