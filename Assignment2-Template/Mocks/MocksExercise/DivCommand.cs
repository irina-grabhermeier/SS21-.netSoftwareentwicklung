using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    public class DivCommand : ICommand
    {
        private readonly string COMMAND = "div";

        public bool Accept(string command, List<string> parameters)
        {
            return command == COMMAND;
        }

        public void Execute(List<string> parameters)
        {
            if (parameters.Count != 2)
            {
                throw new InvalidCommandInputException("Command expects exactly 2 parameter.");
            }

            double nom, denom;
            try
            {
                double.TryParse(parameters[0], out nom);
                double.TryParse(parameters[1], out denom);

                double result = nom / denom;
                Console.WriteLine(result);
            }
            catch (Exception) // We do not use the exception object, by not assigning it a variable it is discarded
            {
                throw new InvalidCommandInputException("Invalid input could not parse number.");
            }
        }
    }
}
