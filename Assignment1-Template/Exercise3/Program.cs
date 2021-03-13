using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1_Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenMapper = new ExpressionTokenMapper();
            var tokenizer = new ExpressionTokenizer(tokenMapper);

            var parser = new ExpressionParser(tokenizer);
            while (true)
            {
                var line = Console.ReadLine();
                try
                {
                    bool ret = parser.ParseExpression(line);
                    Console.WriteLine("Expression is valid...");
                }
                catch (ParseException ex)
                {
                    if (ex.Reason == EExceptionReason.UnmatchedError)
                    {
                        Console.WriteLine($"Parse error at position {ex.Position}: Unmatched \"{ex.Token}\" in expression.");
                    }
                    else if (ex.Reason == EExceptionReason.StartExpressionError)
                    {
                        Console.WriteLine($"Parse error at position {ex.Position}: An expression must start with a digit or a \"(\".");
                    }
                }
                catch (InvalidTokenException ex)
                {
                    var message = $"Unexpected input after \"{ex.TokenData}\" at position {ex.Position}: "
                        + $"expected: \")\",\"+\",\"-\",\"*\",\"/\" or end of expression, but found \"{ex.PeekedData}\".";
                    Console.WriteLine(message);

                }
                catch (TokenizationException ex)
                {
                    Console.WriteLine($"Error at position {ex.CharacterIndex}: \"{ex.Input}\" is not a valid input.");
                }
            }
        }
    }
}
