using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    class Program
    {
        static void Main(string[] args) { 

            ExpressionDemo demo = new ExpressionDemo();
    

            demo.ParseExpression<int>(x => x.CompareTo(5));
        
            var inputReader = new ConsoleInputReader();
            var primeChecker = new SimplePrimeChecker();
            var isPrimeCommand = new IsPrimeCommand(primeChecker);
            var divCommand = new DivCommand();

            var commandContainer = new CommandContainer(inputReader);
            commandContainer.AddCommand(isPrimeCommand);
            commandContainer.AddCommand(divCommand);

            inputReader.StartInputLoop();
        }
    }
}
