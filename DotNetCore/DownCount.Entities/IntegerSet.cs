using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownCount.Entities
{
    /// <summary>
    /// Representation of a class of numbers
    /// </summary>
    public class IntegerSet : List<int>
    {
        #region consructors

        public IntegerSet()
        {
        }

        public IntegerSet(params int[] numbers)
        {
            foreach (int n in numbers)
            {
                Add(n);
            }
        }

        public IntegerSet(IEnumerable<int> numbers)
        {
            foreach (int n in numbers)
            {
                Add(n);
            }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Return a new set of integers not including the ith number where i is a zero based index
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public IntegerSet SubsetNotIncluding(int i)
        {
            if (i < 0 || i >= this.Count)
                throw new ArgumentOutOfRangeException();

            IntegerSet subset = new IntegerSet();
            for (int j = 0; j < this.Count; ++j)
            {
                if (j != i)
                {
                    subset.Add(this[j]);
                }
            }

            return subset;
        }

        /// <summary>
        /// String representation of the set
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("{");
            for (int i = 0; i < this.Count; ++i)
            {
                if (i == this.Count - 1)
                    result.AppendFormat("{0}", this[i].ToString());
                else
                    result.AppendFormat("{0}, ", this[i]);
            }

            result.Append("}");

            return result.ToString();

        }
        #endregion

        #region operator overloads

        public static IntegerSet operator +(IntegerSet set, int n)
        {
            return new IntegerSet(from x in set select x + n);
        }

        public static IntegerSet operator *(IntegerSet set, int n)
        {
            return new IntegerSet(from x in set select x * n);
        }

        public static IntegerSet operator -(IntegerSet set, int n)
        {
            return new IntegerSet(from x in set select x - n);
        }

        public static IntegerSet operator /(IntegerSet set, int n)
        {
            // only return values where the numbers are divisible
            return new IntegerSet(from x in set where x % n == 0 select x / n);
        }
        #endregion
    }
}
