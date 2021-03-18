using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    /// <summary>
    /// Event arguments of read input.
    /// </summary>
    public class InputReadEventArgs : EventArgs
    {
        private readonly string _inputData;
        public string InputData // Event arguments should always provide only read-only fields
        {
            get
            {
                return _inputData;
            }
        }

        public InputReadEventArgs(string inputData)
        {
            _inputData = inputData; // This is the only time we can change the value of a readonly variable
        }


    }
}
