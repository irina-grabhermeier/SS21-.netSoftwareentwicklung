using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MocksExercise;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class CommandContainerUnitTests
    {

        /// <summary>
        /// Test if add checks for invalid inputs.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))] // Tell the runtime to expect an exception to be thrown in this test
        public void TestAdd()
        {
            // We can not use the ConsoleInputReader to do most of the testing of the CommandContainer.
            // For one, it uses an infinite loop and also it expects user inputs. Tests should run
            // automatically without user input. We therefore create a MOCK of the interface. In this case
            // the MOCK does nothing.
            //
            var inputReaderMock = new Mock<IInputReader>();
            var commandContainer = new CommandContainer(inputReaderMock.Object); // supply the mock object to the class

            // The test expects this call to throw an ArgumentNullException. If this call does not throw
            // this exact exception, the test will fail. 
            //
            commandContainer.AddCommand(null);
        }

        /// <summary>
        /// This method tests the correct dispatching of commands by the CommandContainer.
        /// 
        /// </summary>
        [TestMethod]
        public void TestCommandDispatch()
        {
            var inputReaderMock = new Mock<IInputReader>();
            // Create a fake command. We want to test if it is executed.
            //
            var commandMock = new Mock<ICommand>();
            // Let accept return false always.
            //
            commandMock.Setup(x => x.Accept(
                    It.IsAny<string>(),
                    It.IsAny<List<string>>()))
                .Returns(false);

            var commandContainer = new CommandContainer(inputReaderMock.Object);
            commandContainer.AddCommand(commandMock.Object);

            // Define our MOCK functionality of the StartInputLoop method. If this method is called
            // our MOCK will act as a user providing a single input.
            //
            inputReaderMock
                .Setup(x => x.StartInputLoop()).Raises((x) => 
                    // Notice this += is a trick the Mock library uses to fire events. 
                    x.InputEntered += null, this, new InputReadEventArgs("unknown command"));

            // Start the fake input loop, this will simulate a user input of "unknown command".
            // We expect the commandMock to not accept the input. The MOCK library will remember what happened.
            //
            inputReaderMock.Object.StartInputLoop();

            // We now check if our methods were called with the correct input. We tested what would happen if
            // the user entered "unknown command".
            // We expect that Accept() was called on the command mock once, and Execute() never.
            //

            // Set up the Execute() method of the mock, we expect it to be never called in this test. 
            //
            commandMock.Verify(x =>
                    x.Execute(It.IsAny<List<string>>()), // The It class can be used to check the parameters supplied to the method.
                Times.Never); // We expect the execute to not be called for the wrong command.

            // Set up the mock for the Accept() method and add some constraints we expect on the parameter.
            // This lets us test with what parameters and how often a method was called.
            //
            commandMock.Verify(x => x.Accept(
                    // Tests the first parameter was of value "unknown"
                    It.Is<string>((input) => input == "unknown"),
                    // Tests the input was correctly split and the argument is a list containing one
                    // entry with the value "command"
                    It.Is<List<string>>((input) => input.Count == 1 && input[0] == "command")),
                Times.Once // We expect this method to be called exactly once.
            );
        }

        /// <summary>
        /// This method tests command dispatch if an empty line was entered.
        /// 
        /// </summary>
        [TestMethod]
        public void TestCommandDispatchEmptyLine()
        {
            var inputReaderMock = new Mock<IInputReader>();
            // Create a fake command. We want to test if it is executed.
            //
            var commandMock = new Mock<ICommand>();

            // Let accept return false always.
            //
            commandMock.Setup(x => x.Accept(
                    It.IsAny<string>(),
                    It.IsAny<List<string>>()))
                .Returns(false);

            var commandContainer = new CommandContainer(inputReaderMock.Object);
            commandContainer.AddCommand(commandMock.Object);

            // Define our MOCK functionality of the StartInputLoop method. If this method is called
            // our MOCK will act as a user providing a single input.
            //
            inputReaderMock
                .Setup(x => x.StartInputLoop()).Raises((x) =>
                    // Notice this += is a trick the Mock library uses to fire events. 
                    x.InputEntered += null, this, new InputReadEventArgs(""));

            // Start the fake input loop, this will simulate a user input of "unknown command".
            // We expect the commandMock to not accept the input. The MOCK library will remember what happened.
            //
            inputReaderMock.Object.StartInputLoop();

            // We now check if our methods were called with the correct input. We tested what would happen if
            // the user entered "unknown command".
            // We expect that Accept() was called on the command mock once, and Execute() never.
            //

            // Set up the Execute() method of the mock, we expect it to be never called in this test. 
            //
            commandMock.Verify(x =>
                    x.Execute(It.IsAny<List<string>>()), // The It class can be used to check the parameters supplied to the method.
                Times.Never); // We expect the execute to not be called for the wrong command.

            // Set up the mock for the Accept() method and add some constraints we expect on the parameter.
            // This lets us test with what parameters and how often a method was called.
            //
            commandMock.Verify(x => x.Accept(
                    // Tests the first parameter is not null
                    It.IsNotNull<string>(),
                    // Test the second parameter is an empty list
                    It.Is<List<string>>( (input) => input != null && input.Count == 0)),
                Times.Once // We expect this method to be called exactly once.
            );
        }

        /// <summary>
        /// This method tests that the Execute() method is correctly called if Accept() returned true.
        /// 
        /// </summary>
        [TestMethod]
        public void TestCommandDispatchExecute()
        {
            var inputReaderMock = new Mock<IInputReader>();
            // Create a fake command. We want to test if it is executed.
            //
            var commandMock = new Mock<ICommand>();
            
            // Let accept retun true  if the command is "command" and args is anything.
            //
            commandMock.Setup(x => x.Accept(
                It.Is<string>( input => input == "command"), 
                It.IsAny<List<string>>()))
                .Returns(true);

            var commandContainer = new CommandContainer(inputReaderMock.Object);
            commandContainer.AddCommand(commandMock.Object);

            // Define our MOCK functionality of the StartInputLoop method. If this method is called
            // our MOCK will act as a user providing a single input.
            //
            inputReaderMock
                .Setup(x => x.StartInputLoop()).Raises((x) =>
                    // Notice this += is a trick the Mock library uses to fire events. 
                    x.InputEntered += null, this, new InputReadEventArgs("command arg"));

            // Start the fake input loop, this will simulate a user input of "unknown command".
            // We expect the commandMock to not accept the input. The MOCK library will remember what happened.
            //
            inputReaderMock.Object.StartInputLoop();

            // We now check if our methods were called with the correct input. We tested what would happen if
            // the user entered "unknown command".
            // We expect that Accept() was called on the command mock once, and Execute() never.
            //

            // Set up the Execute() method of the mock, we expect it to be never called in this test. 
            //
            commandMock.Verify(x =>
                    x.Execute(It.Is<List<string>>( input => input != null && input.Count == 1 && input[0] == "arg")),
                Times.Once); // We expect the execute to be called once.

            // Set up the mock for the Accept() method and add some constraints we expect on the parameter.
            // This lets us test with what parameters and how often a method was called.
            //
            commandMock.Verify(x => x.Accept(
                    It.IsAny<string>(),
                    It.IsAny<List<string>>()),
                Times.Once // We expect this method to be called exactly once.
            );
        }



    }
}
