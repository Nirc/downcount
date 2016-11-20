using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownCount.Entities.Equations
{
    /// <summary>
    /// I wish to represent all equations (involving integers) as an object deriving from IEquation.
    /// </summary>
    public interface IEquation
    {
        decimal Value { get; }
    }
}
