using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DownCount.Entities.Equations;

namespace DownCount.Entities
{
    /// <summary>
    /// Class to represent a set of solutions to a game of DownCount
    /// </summary>
    public class Solutions : List<IEquation>
    {
        public Solutions()
        {
        }

        #region operator overloads

        /// <summary>
        /// Return a new set of solutions consisting of the original solution + equation
        /// </summary>
        /// <param name="solutions"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        public static Solutions operator +(Solutions solutions, IEquation equation)
        {
            Solutions result = new Solutions();
            solutions.ForEach(eq => result.Add(new AddOperation(eq, equation)));
            return result;
        }


        /// <summary>
        /// Return a new set of solutions consisting of the original solution - equation
        /// </summary>
        /// <param name="solutions"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        public static Solutions operator -(Solutions solutions, IEquation equation)
        {
            Solutions result = new Solutions();
            solutions.ForEach(eq => result.Add(new MinusOperation(eq, equation)));
            return result;
        }


        /// <summary>
        /// Return a new set of solutions consisting of equation  - the original solution
        /// </summary>
        /// <param name="solutions"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        public static Solutions operator -(IEquation equation, Solutions solutions)
        {
            Solutions result = new Solutions();
            solutions.ForEach(eq => result.Add(new MinusOperation(equation, eq)));
            return result;
        }


        /// <summary>
        /// Return a new set of solutions consisting of the original solution * equation
        /// </summary>
        /// <param name="solutions"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        public static Solutions operator *(Solutions solutions, IEquation equation)
        {
            Solutions result = new Solutions();
            solutions.ForEach(eq => result.Add(new MultiplyOperation(eq, equation)));
            return result;
        }


        /// <summary>
        /// Return a new set of solutions consisting of the original solutions / equation
        /// </summary>
        /// <param name="solutions"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        public static Solutions operator /(Solutions solutions, IEquation equation)
        {
            Solutions result = new Solutions();
            foreach (IEquation eq in solutions)
            {
                //if(eq.Value % equation.Value == 0)
                result.Add(new DivideOperation(eq, equation));
            }
            return result;
        }


        /// <summary>
        /// Return a new set of solutions consisting of equation / the original solutions
        /// </summary>
        /// <param name="solutions"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        public static Solutions operator /(IEquation equation, Solutions solutions)
        {
            Solutions result = new Solutions();
            foreach (IEquation eq in solutions)
            {
                //if (equation.Value % eq.Value == 0)
                result.Add(new DivideOperation(equation, eq));
            }
            return result;
        }
        #endregion
    }
}
