using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1_Exceptions
{
    public class ParseException : Exception
    {
        public EExceptionReason Reason { get; }
        public string Token { get; }
        public int Position { get; }

        public ParseException(EExceptionReason reason, string token, int position)
        {
            Reason = reason;
            Token = token;
            Position = position;
        }

        public ParseException(EExceptionReason reason, int position)
        {
            Reason = reason;
            Token = "";
            Position = position;
        }
    }
}
