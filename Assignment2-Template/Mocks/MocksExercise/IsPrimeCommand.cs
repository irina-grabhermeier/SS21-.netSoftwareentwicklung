using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    public class IsPrimeCommand : ICommand
    {
        private readonly string COMMAND = "isprime";
        private readonly IPrimeChecker<ulong> _primeChecker;

        public IsPrimeCommand(IPrimeChecker<ulong> primeChecker)
        {
            _primeChecker = primeChecker;
        }

        public bool Accept(string command, List<string> parameters)
        {
            if (command != COMMAND)
            {
                return false;
            }
            return true;
        }

        public void Execute(List<string> parameters)
        { 
            if (parameters.Count == 0)
            {   
                throw new InvalidCommandInputException("Command expects at least 1 parameter.");
            }
            CheckParameters(parameters);

            foreach (var param in parameters)
            {
                ulong num = ulong.Parse(param);
                if (_primeChecker.IsPrime(num))
                {
                    Console.WriteLine($"{num} is prime.");
                }
                else
                {
                    Console.WriteLine($"{num} is no prime.");
                }
            }
        }

        /// <summary>
        /// This is a protected method. It can be tricky to test these directly. Protected methods
        /// can be used by classes derived from this class.
        /// </summary>
        private void CheckParameters(List<string> parameters)
        {
            foreach (var param in parameters)
            {
                var sb = new StringBuilder();
                sb.Append("Input-Error: ");

                bool accept = true;
                ulong result;
                if (ulong.TryParse(param, out result) == false)
                {
                    accept = false;
                    sb.AppendLine("Invalid parameter: " + param);
                }

                if (accept == false)
                {
                    throw new InvalidCommandInputException(sb.ToString());
                }
            }
        }
    }
}
