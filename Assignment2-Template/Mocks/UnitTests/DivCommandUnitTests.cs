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
    public class DivCommandUnitTests
    {
        [TestMethod]
        public void TestAccept1()
        {
            var obj = new DivCommand();

            // We expect the method to return true.
            //
            Assert.IsTrue(obj.Accept("div", new List<string> { }));
        }

        [TestMethod]
        public void TestAccept2()
        {
            var obj = new DivCommand();

            // We expect the method to return true.
            //
            Assert.IsFalse(obj.Accept("command", new List<string> { }));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandInputException))]
        public void TestExecute1()
        {
            var obj = new DivCommand();

            // We expect an exception because we are missing any arguments.
            //
            obj.Execute(new List<string> { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandInputException))]
        public void TestExecute2()
        {
            var obj = new DivCommand();

            // We expect an exception because we are not allowed to divide by zero. 
            //
            obj.Execute(new List<string> { "1", "0" });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandInputException))]
        public void TestExecute3()
        {
            var obj = new DivCommand();

            // We expect an exception because we need exactly two arguments. 
            //
            obj.Execute(new List<string> { "1" });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandInputException))]
        public void TestExecute4()
        {
            var obj = new DivCommand();

            // We expect an exception because we need exactly two arguments. 
            //
            obj.Execute(new List<string> { "1", "2", "3" });
        }
    }
}
