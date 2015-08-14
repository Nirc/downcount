using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DownCount.Entities.Equations;

namespace DownCount.Entities.Test
{
    [TestClass]
    public class Equations_Test
    {
        [TestMethod]
        public void Equation_Test1()
        {
            //Try to represent the equation ((5+1) x 3) - 4
            IEquation node1 = new AddOperation(new Integer(5), new Integer(1));
            Assert.AreEqual(6, node1.Value);

            MultiplyOperation node2 = new MultiplyOperation(node1, new Integer(3));
            Assert.AreEqual(18, node2.Value);

            MinusOperation equation = new MinusOperation(node2, new Integer(4));

            Assert.AreEqual(14, equation.Value);
            Assert.AreEqual("((5 + 1) x 3) - 4", equation.ToString());
        }

        [TestMethod]
        public void Equation_Test2()
        {
            //Try to represent the equation 1 + 2 -3
            IEquation node1 = new AddOperation(new Integer(1), new Integer(2));
            Assert.AreEqual(3, node1.Value);

            AddOperation equation = new AddOperation(node1, new Integer(3));
            Assert.AreEqual(6, equation.Value);
            Assert.AreEqual("1 + 2 + 3", equation.ToString());
        }

        [TestMethod]
        public void Equation_Test3()
        {
            //Try to represent the equation ((6 / 3) + 2) x 5
            IEquation node1 = new DivideOperation(new Integer(6), new Integer(3));
            Assert.AreEqual(2, node1.Value);

            IEquation node2 = new AddOperation(node1, new Integer(2));
            Assert.AreEqual(4, node2.Value);

            IEquation equation = new MultiplyOperation(node2, new Integer(5));

            Assert.AreEqual(20, equation.Value);
            Assert.AreEqual("((6 / 3) + 2) x 5", equation.ToString());
        }
    }
}
