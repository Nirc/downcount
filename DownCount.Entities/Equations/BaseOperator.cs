using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownCount.Entities.Equations
{
    /// <summary>
    /// An abstract base class representing an operation on two IEquation objects. 
    /// </summary>
    public abstract class BaseOperator : IEquation
    {
        public BaseOperator(IEquation x, IEquation y, Func _fn, string _symbol)
        {
            LHS = x;
            RHS = y;
            fn = _fn;
            symbol = _symbol;
        }

        public decimal Value
        {
            get
            {
                return fn(LHS.Value, RHS.Value);
            }
        }

        public override string ToString()
        {
            bool showBrackets = true;
            string lhs = string.Empty;           

            if (LHS is Integer)
                showBrackets = false;
                        
            if ((LHS is AddOperation || LHS is MinusOperation) && (this is AddOperation || this is MinusOperation) ||
                (LHS is MultiplyOperation && this is MultiplyOperation))
                showBrackets = false;

            if (showBrackets)
                lhs = string.Format("({0})", LHS.ToString());
            else
                lhs = LHS.ToString();

            showBrackets = true;
            string rhs = string.Empty;

            if (RHS is Integer)
                showBrackets = false;

            if ((this is AddOperation  && RHS is AddOperation) ||
                (this is AddOperation && RHS is MinusOperation) ||
                (RHS is MultiplyOperation && this is MultiplyOperation))
                showBrackets = false;

            if (showBrackets)
                rhs = string.Format("({0})", RHS.ToString());
            else
                rhs = RHS.ToString();

           return string.Format("{0} {1} {2}", lhs, symbol, rhs);  
        }

        #region instance data

        public delegate decimal Func(decimal x, decimal y);

        protected readonly IEquation LHS;
        protected readonly IEquation RHS;
        protected readonly Func fn;
        protected readonly string symbol;

        #endregion
    }
}
