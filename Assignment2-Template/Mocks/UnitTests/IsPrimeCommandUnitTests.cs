using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MocksExercise;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class IsPrimeCommandUnitTests
    {
        [TestMethod]
        public void TestAccept1()
        {
            var primeCheckerMock = new Mock<IPrimeChecker<ulong>>();
            var obj = new IsPrimeCommand(primeCheckerMock.Object);

            // We expect the method to return true.
            //
            Assert.IsTrue(obj.Accept("isprime", new List<string> { }));
        }

        [TestMethod]
        public void TestAccept2()
        {
            var primeCheckerMock = new Mock<IPrimeChecker<ulong>>();
            var obj = new IsPrimeCommand(primeCheckerMock.Object);

            // We expect the method to return true.
            //
            Assert.IsFalse(obj.Accept("command", new List<string> { }));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandInputException))]
        public void TestExecute1()
        {
            var primeCheckerMock = new Mock<IPrimeChecker<ulong>>();
            var obj = new IsPrimeCommand(primeCheckerMock.Object);

            // We expect an exception because we are missing any arguments.
            //
            obj.Execute(new List<string> { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandInputException))]
        public void TestExecute2()
        {
            var primeCheckerMock = new Mock<IPrimeChecker<ulong>>();
            var obj = new IsPrimeCommand(primeCheckerMock.Object);

            // We expect an exception because negative numbers are not supported.
            //
            obj.Execute(new List<string> { "-1"});
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandInputException))]
        public void TestExecute3()
        {
            var primeCheckerMock = new Mock<IPrimeChecker<ulong>>();
            var obj = new IsPrimeCommand(primeCheckerMock.Object);

            // We expect an exception because arguments can not be converted into numbers.
            //
            obj.Execute(new List<string> { "2", "abcd" });
        }

    }
}
