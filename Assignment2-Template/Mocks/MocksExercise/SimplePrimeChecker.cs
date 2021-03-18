using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    /// <summary>
    /// This class checks if an input is a prime number. 
    /// Prime numbers are all positive integers larger than 1
    /// that divisible only by 1 and themselves. 
    /// </summary>
    public class SimplePrimeChecker : IPrimeChecker<ulong>
    {
        public bool IsPrime(ulong input)
        {
            for (ulong n = input - 1; n > 1; n--)
            {
                if (input % n == 0)
                {
                    return false;
                }
            }

            return true;
        }

        
    }
}
