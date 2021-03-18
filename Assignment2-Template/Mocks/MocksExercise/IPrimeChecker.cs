using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    public interface IPrimeChecker<T>
    {
        bool IsPrime(T input);
    }
}
