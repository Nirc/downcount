using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DownCount.Entities;
using DownCount.Entities.Equations;
using DownCount.Engine;

namespace DownCount.Engine.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DownCountSolver_Test
    {
        public DownCountSolver_Test()
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
        public void DownCountSolver_Test1()
        {
            DownCountGame game = new DownCountGame(6, 1, 2, 3);
            Solutions solutions = DownCountSolver.Solve(game);
            Assert.IsTrue(solutions.Count > 0);
            solutions.ForEach(solution => Assert.AreEqual(6, solution.Value));

            solutions = DownCountSolver.Solve(game, false);
            Assert.IsTrue(solutions.Count == 1);
            solutions.ForEach(solution => Assert.AreEqual(6, solution.Value));
        }

        [TestMethod]
        public void DownCountSolver_Test2()
        {
            DownCountGame game = new DownCountGame(250, 50, 4, 1);
            Solutions solutions = DownCountSolver.Solve(game);
            Assert.IsTrue(solutions.Count > 0);
            solutions.ForEach(solution => Assert.AreEqual(250, solution.Value));

            solutions = DownCountSolver.Solve(game, false);
            Assert.IsTrue(solutions.Count == 1);
            solutions.ForEach(solution => Assert.AreEqual(250, solution.Value));
        }

        [TestMethod]
        public void DownCountSolver_Test3()
        {
            DownCountGame game = new DownCountGame(577, 75, 9, 3, 4, 2, 1);
            Solutions solutions = DownCountSolver.Solve(game);
            Assert.IsTrue(solutions.Count > 0);
            solutions.ForEach(solution => Assert.AreEqual(577, solution.Value));

            solutions = DownCountSolver.Solve(game, false);
            Assert.IsTrue(solutions.Count == 1);
            solutions.ForEach(solution => Assert.AreEqual(577, solution.Value));
        }

        [TestMethod]
        public void DownCountSolver_Test4()
        {
            DownCountGame game = new DownCountGame(952, 25, 50, 75, 100, 3, 6);
            Solutions solutions = DownCountSolver.Solve(game);
            Assert.IsTrue(solutions.Count > 0);
            solutions.ForEach(solution => Assert.AreEqual(952, solution.Value));

            solutions = DownCountSolver.Solve(game, false);
            Assert.IsTrue(solutions.Count == 1);
            solutions.ForEach(solution => Assert.AreEqual(952, solution.Value));
        }
        [TestMethod]
        public void DownCountSolver_NoSolution()
        {
            DownCountGame game = new DownCountGame(577, 9, 3, 4, 2, 1);
            Solutions solutions = DownCountSolver.Solve(game);
            Assert.IsTrue(solutions.Count ==  0);
        }
    }
}
