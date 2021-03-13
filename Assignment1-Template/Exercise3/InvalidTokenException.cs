using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1_Exceptions
{
    public class InvalidTokenException : Exception
    {
        public string PeekedData { get; }
        public int Position { get; }
        public string TokenData { get; }
        public InvalidTokenException(string peekedData, int position, string tokenData)
        {
            PeekedData = peekedData;
            Position = position;
            TokenData = tokenData;
        }
    }
}
