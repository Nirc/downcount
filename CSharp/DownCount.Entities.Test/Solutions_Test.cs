using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DownCount.Entities.Equations;
using DownCount.Entities;

namespace DownCount.Entities.Test
{
    [TestClass]
    public class Solutions_Test
    {
        [TestMethod]
        public void Solutions_AddOperator_Test()
        {
            //Try to represent the equation ((5+1) x 3) - 4
            IEquation node1 = new AddOperation(new Integer(5), new Integer(1));
            IEquation node2 = new MultiplyOperation(node1, new Integer(3));
            IEquation equation1 = new MinusOperation(node2, new Integer(4));

            //Try to represent the equation 1 + 2  +3
            IEquation node3 = new AddOperation(new Integer(1), new Integer(2));
            IEquation equation2 = new AddOperation(node3, new Integer(3));

            Solutions solutions = new Solutions();
            solutions.Add(equation1);
            solutions.Add(equation2);

            IEquation equation3 = new Integer(4);

            Solutions new_solutions = solutions + equation3;
            Assert.AreEqual(2, new_solutions.Count);
            Assert.AreEqual(18, new_solutions[0].Value);
            Assert.AreEqual(10, new_solutions[1].Value);
        }

        [TestMethod]
        public void Solutions_MinusOperator_Test()
        {
            //Try to represent the equation ((5+1) x 3) - 4
            IEquation node1 = new AddOperation(new Integer(5), new Integer(1));
            IEquation node2 = new MultiplyOperation(node1, new Integer(3));
            IEquation equation1 = new MinusOperation(node2, new Integer(4));

            //Try to represent the equation 1 + 2  +3
            IEquation node3 = new AddOperation(new Integer(1), new Integer(2));
            IEquation equation2 = new AddOperation(node3, new Integer(3));

            Solutions solutions = new Solutions();
            solutions.Add(equation1);
            solutions.Add(equation2);

            IEquation equation3 = new Integer(4);

            Solutions new_solutions = solutions - equation3;
            Assert.AreEqual(2, new_solutions.Count);
            Assert.AreEqual(10, new_solutions[0].Value);
            Assert.AreEqual(2, new_solutions[1].Value);

            new_solutions = equation3 - solutions;
            Assert.AreEqual(2, new_solutions.Count);
            Assert.AreEqual(-10, new_solutions[0].Value);
            Assert.AreEqual(-2, new_solutions[1].Value);
        }

        [TestMethod]
        public void Solutions_MultiplyOperator_Test()
        {
            //Try to represent the equation ((5+1) x 3) - 4
            IEquation node1 = new AddOperation(new Integer(5), new Integer(1));
            IEquation node2 = new MultiplyOperation(node1, new Integer(3));
            IEquation equation1 = new MinusOperation(node2, new Integer(4));

            //Try to represent the equation 1 + 2  +3
            IEquation node3 = new AddOperation(new Integer(1), new Integer(2));
            IEquation equation2 = new AddOperation(node3, new Integer(3));

            Solutions solutions = new Solutions();
            solutions.Add(equation1);
            solutions.Add(equation2);

            IEquation equation3 = new Integer(4);

            Solutions new_solutions = solutions * equation3;
            Assert.AreEqual(2, new_solutions.Count);
            Assert.AreEqual(56, new_solutions[0].Value);
            Assert.AreEqual(24, new_solutions[1].Value);
        }

        [TestMethod]
        public void Solutions_DivideOperator_Test()
        {
            //Try to represent the equation ((5+1) x 3) - 4
            IEquation node1 = new AddOperation(new Integer(5), new Integer(1));
            IEquation node2 = new MultiplyOperation(node1, new Integer(3));
            IEquation equation1 = new MinusOperation(node2, new Integer(4));

            //Try to represent the equation 1 + 2  +3
            IEquation node3 = new AddOperation(new Integer(1), new Integer(2));
            IEquation equation2 = new AddOperation(node3, new Integer(3));

            Solutions solutions = new Solutions();
            solutions.Add(equation1);
            solutions.Add(equation2);

            IEquation equation3 = new Integer(14);

            Solutions new_solutions = solutions / equation3;
            Assert.AreEqual(2, new_solutions.Count);
            Assert.AreEqual(1, new_solutions[0].Value);
            Assert.AreEqual((decimal)6/(decimal)14, new_solutions[1].Value);

            new_solutions = solutions / new Integer(3);
            Assert.AreEqual(2, new_solutions.Count);
            Assert.AreEqual((decimal)14 / (decimal)3, new_solutions[0].Value);
            Assert.AreEqual(2, new_solutions[1].Value);
        }

    }
}
