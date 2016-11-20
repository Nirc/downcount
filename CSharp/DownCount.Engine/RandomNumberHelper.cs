using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownCount.Engine
{
    public class RandomNumberHelper
    {
        private static readonly Random rand = new Random();

        public static int RandomIntegerFromSet(params int[] list)
        {
            int num = rand.Next(0, list.Length - 1);
            return list[num];
        }

        public static int RandomInteger(int minValue, int maxValue)
        {
            return rand.Next(minValue, maxValue);
        }
    }
}
