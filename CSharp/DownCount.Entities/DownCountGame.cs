using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownCount.Entities
{
    /// <summary>
    /// Class which represents all the elements needed in a game of downcount
    /// </summary>
    public class DownCountGame
    {
        public DownCountGame()
        {
        }

        public DownCountGame(int target, params int[] numbers)
        {
            TargetNumber = target;
            Numbers = new IntegerSet(numbers);
        }

        /// <summary>
        /// The number we are trying to solve
        /// </summary>
        public int TargetNumber { get; set; }

        /// <summary>
        /// The set of numbers from which we must make Target Number
        /// </summary>
        public IntegerSet Numbers { get; set; }

    }
}
