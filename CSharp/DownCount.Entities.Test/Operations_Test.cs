using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DownCount.Entities.Equations;

namespace DownCount.Entities.Test
{
    /// <summary>
    /// Summary description for Operations_Test
    /// </summary>
    [TestClass]
    public class Operations_Test
    {
        public Operations_Test()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddOperation_Test()
        {
            AddOperation op = new AddOperation(new Integer(2), new Integer(4));
            Assert.AreEqual(6, op.Value);
            Assert.AreEqual("2 + 4", op.ToString());
        }

        [TestMethod]
        public void MinusOperation_Test()
        {
            MinusOperation op = new MinusOperation(new Integer(2), new Integer(4));
            Assert.AreEqual(-2, op.Value);
            Assert.AreEqual("2 - 4", op.ToString());
        }

        [TestMethod]
        public void MultiplyOperation_Test()
        {
            MultiplyOperation op = new MultiplyOperation(new Integer(2), new Integer(4));
            Assert.AreEqual(8, op.Value);
            Assert.AreEqual("2 x 4", op.ToString());
        }

        [TestMethod]
        public void DivideOperation_Test()
        {
            DivideOperation op = new DivideOperation(new Integer(4), new Integer(2));
            Assert.AreEqual(2, op.Value);
            Assert.AreEqual("4 / 2", op.ToString());

            op = new DivideOperation(new Integer(2), new Integer(4));
            Assert.AreEqual((decimal)0.5, op.Value);
            Assert.AreEqual("2 / 4", op.ToString());
        }
    }
}
