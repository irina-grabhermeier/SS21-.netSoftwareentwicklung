using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    public interface ICommand
    {
        bool Accept(string command, List<string> parameters);
        void Execute(List<string> parameters);
    }
}
