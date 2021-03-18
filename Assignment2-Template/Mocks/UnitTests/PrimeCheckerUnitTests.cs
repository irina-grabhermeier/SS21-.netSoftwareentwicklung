using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MocksExercise;

namespace UnitTests
{
    /// <summary>
    /// This class provides unit tests to verify the functionality of the prime checker. 
    /// </summary>
    [TestClass]
    public class SimplePrimeCheckerUnitTests
    {
        [TestMethod]
        public void TestCheck()
        {
            var primeChecker = new SimplePrimeChecker();

            Assert.IsFalse(primeChecker.IsPrime(1));
            Assert.IsFalse(primeChecker.IsPrime(4));

            Assert.IsTrue(primeChecker.IsPrime(2));
            Assert.IsTrue(primeChecker.IsPrime(11));
            Assert.IsTrue(primeChecker.IsPrime(199));
            Assert.IsTrue(primeChecker.IsPrime(1259));
            Assert.IsTrue(primeChecker.IsPrime(3271));
        }
    }
}
