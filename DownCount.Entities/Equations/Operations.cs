using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownCount.Entities.Equations
{
    ////////////////////////////////////////////////////////////////////////////////
    // The concrete implementations of  the four main inteher operations +, -, *, /
    ////////////////////////////////////////////////////////////////////////////////

    public class AddOperation : BaseOperator
    {
        public AddOperation(IEquation x, IEquation y)
            : base(x, y, (a,b) => a + b, "+")
        {
        }
    }

    public class MinusOperation : BaseOperator
    {
        public MinusOperation(IEquation x, IEquation y)
            : base(x, y, (a, b) => a - b, "-")
        {
        }
    }

    public class MultiplyOperation : BaseOperator
    {
        public MultiplyOperation(IEquation x, IEquation y)
            : base(x, y, (a, b) => a * b, "x")
        {
        }
    }

    public class DivideOperation : BaseOperator
    {
        public DivideOperation(IEquation x, IEquation y)
            : base(x, y, (a, b) => a / b, "/")
        {
        }
    }
}
