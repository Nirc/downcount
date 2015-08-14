using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DownCount.Entities.Test
{
    /// <summary>
    /// Summary description for DownCountGame_Test
    /// </summary>
    [TestClass]
    public class DownCountGame_Test
    {
        public DownCountGame_Test()
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
        public void DownCountGame_Constructor_Test()
        {
            DownCountGame game = new DownCountGame(577, 45, 67, 7);
            Assert.AreEqual(577, game.TargetNumber);
            Assert.AreEqual(3, game.Numbers.Count);
            Assert.AreEqual(45, game.Numbers[0]);
            Assert.AreEqual(67, game.Numbers[1]);
            Assert.AreEqual(7, game.Numbers[2]);
        }
    }
}
