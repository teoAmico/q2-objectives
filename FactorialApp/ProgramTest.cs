using FactorialTestCMD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FactorialApp.Test
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        [DataRow(1, 0)]
        public void Factorial_OfZero_ShouldReturnOne(int expected, int number)
        {
            Assert.AreEqual(expected, Program.Factorial(number));
        }

        [TestMethod]
        public void Factorial_OfOne_ShouldReturnOne()
        {
            Assert.AreEqual(1, Program.Factorial(1));
        }

        [TestMethod]
        public void Factorial_OfFive_ShouldReturnOneHundredAndTwenty()
        {
            Assert.AreEqual(120, Program.Factorial(5));
        }

        [TestMethod]
        public void Factorial_OfTen_ShouldReturnThreeMillionSixHundredAndTwentyEightThousandEightHundred()
        {
            Assert.AreEqual(3628800, Program.Factorial(10));
        }

        [TestMethod]
        public void Factorial_OfNegativeNumber_ShouldReturnOne()
        {
            Assert.AreEqual(1, Program.Factorial(-5));
        }
    }
}
