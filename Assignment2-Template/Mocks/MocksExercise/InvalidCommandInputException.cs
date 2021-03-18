using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    public class InvalidCommandInputException : Exception
    {
        public InvalidCommandInputException(string errorMessage) :
            base(errorMessage) // This calls the constructor of the base class.
        {

        }
    }
}
