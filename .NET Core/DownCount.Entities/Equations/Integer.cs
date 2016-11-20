using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownCount.Entities.Equations
{
    /// <summary>
    /// The simplest equation can be a standalone integer. This class makes these equations representable under the IEquation framwork
    /// </summary>
    public class Integer : IEquation
    {
        public Integer(int number)
        {
            _number = number;
        }

        public decimal Value 
        { 
            get 
            {
                return _number;
            } 
        }
        private int _number;

        public override string ToString()
        {
            return _number.ToString();
        }
    }
}
