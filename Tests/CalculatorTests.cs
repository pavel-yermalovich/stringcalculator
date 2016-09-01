using System;
using NUnit.Framework;
using StringCalculator;

namespace Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        public Calculator Calculator = new Calculator();

        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2,3", 6)]
        [TestCase("1\n2\n3", 6)]
        [TestCase("//x\n1x2x3", 6)]
        [TestCase("//[xyz][sw][abb]\n1xyz2sw3abb0", 6)]
        [TestCase("1,2,3,3000", 6)]
        public void Add_Should_Return_Expected_Result(string input, int expected)
        {
            AddAndAssert(input, expected);
        }

        [TestCase("1,2,-3", "negatives not allowed: -3")]
        [TestCase("1,2,-3,-4", "negatives not allowed: -3, -4")]
        public void Negatives_Should_Troughs_Exception(string input, string expectedMessage)
        {
            TestDelegate test = () => Calculator.Add(input);

            var exception = Assert.Throws<ArgumentException>(test);
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        private void AddAndAssert(string input, int expected)
        {
            int result = Calculator.Add(input);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
