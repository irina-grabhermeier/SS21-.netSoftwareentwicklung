using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    /// <summary>
    /// Defines the functionality of reading input from any source and providing the input to other classes.
    /// </summary>
    public interface IInputReader
    {
        event EventHandler<InputReadEventArgs> InputEntered;
        void StartInputLoop();
    }
}
