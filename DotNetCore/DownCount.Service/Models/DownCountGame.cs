using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownCount.Service.Models
{
    public class DownCountGame
    {
        /// <summary>
        /// The number we are trying to solve
        /// </summary>
        public int TargetNumber { get; set; }

        /// <summary>
        /// The set of numbers from which we must make Target Number
        /// </summary>
        public List<int> Numbers { get; set; }
    }
}
