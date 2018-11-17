namespace Business.Tests
{
    using Business;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FibonacciTests
    {
        [TestMethod]
        public void GetFibNumber_NegativeNumber__ReturnsMinus1()
        {
            Assert.AreEqual(-1, Fibonacci.GetFibNumber(-1));
            Assert.AreEqual(-1, Fibonacci.GetFibNumber(-100));
        }
        
        [TestMethod]
        public void GetFibNumber_Zero__ReturnsMinus1()
        {
            Assert.AreEqual(-1, Fibonacci.GetFibNumber(0));
        }

        [TestMethod]
        public void GetFibNumber_NumberBiggerthan100__ReturnsMinus1()
        {
            Assert.AreEqual(-1, Fibonacci.GetFibNumber(1000));
        }

        [TestMethod]
        public void GetFibNumber_SimpleValues__ReturnsFibonacciValue()
        {
            Assert.AreEqual(1, Fibonacci.GetFibNumber(1));
            Assert.AreEqual(1, Fibonacci.GetFibNumber(2));
            Assert.AreEqual(2, Fibonacci.GetFibNumber(3));
            Assert.AreEqual(55, Fibonacci.GetFibNumber(10));
            Assert.AreEqual(610, Fibonacci.GetFibNumber(15));
        }
    }
}
