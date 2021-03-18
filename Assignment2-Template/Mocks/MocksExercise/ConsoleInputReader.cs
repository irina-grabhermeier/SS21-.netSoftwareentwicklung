using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    public class ConsoleInputReader : IInputReader
    {
        public event EventHandler<InputReadEventArgs> InputEntered = delegate { };

        public void StartInputLoop()
        {
            while (true)
            {
                var input = Console.ReadLine();
                InputEntered(this, new InputReadEventArgs(input));
            }
        }
    }
}
