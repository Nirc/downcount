using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DownCount.Entities;

namespace DownCount.Entities.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class IntegerSet_Test
    {
        public IntegerSet_Test()
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
        public void IntegerSet_Constructor_Test()
        {
            IntegerSet end = new IntegerSet(1, 2, 3, 4, 5);

            Assert.AreEqual(5, end.Count);
            Assert.AreEqual(1, end[0]);
            Assert.AreEqual(2, end[1]);
            Assert.AreEqual(3, end[2]);
            Assert.AreEqual(4, end[3]);
            Assert.AreEqual(5, end[4]);
        }

        [TestMethod]
        public void IntegerSet_ToString_Test()
        {
            IntegerSet end = new IntegerSet(1, 2, 3, 4, 5);

            Assert.AreEqual("{1, 2, 3, 4, 5}", end.ToString());
        } 

        [TestMethod]
        public void IntegerSet_Subset_Test()
        {
            IntegerSet start = new IntegerSet(1, 2, 3, 4, 5);
            IntegerSet end = start.SubsetNotIncluding(1);

            Assert.AreEqual(4, end.Count);
            Assert.AreEqual(1, end[0]);
            Assert.AreEqual(3, end[1]);
            Assert.AreEqual(4, end[2]);
            Assert.AreEqual(5, end[3]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IntegerSet_Subset_ArgumentOutOfRangeTest1()
        {
            IntegerSet start = new IntegerSet(1, 2, 3, 4, 5);
            IntegerSet end = start.SubsetNotIncluding(7);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IntegerSet_Subset_ArgumentOutOfRangeTest2()
        {
            IntegerSet start = new IntegerSet(1, 2, 3, 4, 5);
            IntegerSet end = start.SubsetNotIncluding(-1);
        }

        [TestMethod]
        public void IntegerSet_AddOperator_Test()
        {
            IntegerSet start = new IntegerSet(1, 2, 3, 4, 5);
            IntegerSet end = start + 8;

            Assert.AreEqual(5, end.Count);
            Assert.AreEqual(9, end[0]);
            Assert.AreEqual(10, end[1]);
            Assert.AreEqual(11, end[2]);
            Assert.AreEqual(12, end[3]);
            Assert.AreEqual(13, end[4]);
        }

        [TestMethod]
        public void IntegerSet_SubtractOperator_Test()
        {
            IntegerSet start = new IntegerSet(1, 2, 3, 4, 5);
            IntegerSet end = start - 8;

            Assert.AreEqual(5, end.Count);
            Assert.AreEqual(-7, end[0]);
            Assert.AreEqual(-6, end[1]);
            Assert.AreEqual(-5, end[2]);
            Assert.AreEqual(-4, end[3]);
            Assert.AreEqual(-3, end[4]);
        }

        [TestMethod]
        public void IntegerSet_MultiplyOperator_Test()
        {
            IntegerSet start = new IntegerSet(1, 2, 3, 4, 5);
            IntegerSet end = start * 2;

            Assert.AreEqual(5, end.Count);
            Assert.AreEqual(2, end[0]);
            Assert.AreEqual(4, end[1]);
            Assert.AreEqual(6, end[2]);
            Assert.AreEqual(8, end[3]);
            Assert.AreEqual(10, end[4]);
        }

        [TestMethod]
        public void IntegerSet_DivideOperatorTest1()
        {
            IntegerSet start = new IntegerSet(1, 2, 3, 4, 5);
            IntegerSet end = start / 2;

            Assert.AreEqual(2, end.Count);
            Assert.AreEqual(1, end[0]);
            Assert.AreEqual(2, end[1]);
        }

        [TestMethod]
        public void IntegerSet_DivideOperatorTest2()
        {
            IntegerSet start = new IntegerSet(1, 2, 3, 4, 5);
            IntegerSet end = start / 8;

            Assert.AreEqual(0, end.Count);
        }
    }
}
