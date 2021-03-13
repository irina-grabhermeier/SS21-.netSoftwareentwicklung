using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1_Exceptions
{
    public class ExpressionParser
    {
        private ITokenizer<ExpressionToken> _tokenizer;
        private int _parenthesesCounter = 0;

        public ExpressionParser(ITokenizer<ExpressionToken> tokenizer)
        {
            _tokenizer = tokenizer;
        }

        // This is a very simple parser, that only verifies if the input is correct. 
        //
        public bool ParseExpression(string input)
        {
            var tokenStream = _tokenizer.CreateTokenStream(input);

            if (tokenStream.Peek().TokenType != EExpressionTokenType.Digit &&
                tokenStream.Peek().TokenType != EExpressionTokenType.ParenthesesOpen &&
                tokenStream.Peek().TokenType != EExpressionTokenType.End)
            {
                throw new ParseException(EExceptionReason.StartExpressionError, tokenStream.Peek().StartIndex);
            }

            while (tokenStream.IsFinshed == false)
            {
                var token = tokenStream.Next();
                switch (token.TokenType)
                {

                    case EExpressionTokenType.ParenthesesOpen: // next token must be a digit or another paranthesis open
                        _parenthesesCounter++;
                        {
                            var peeked = tokenStream.Peek();
                            if (peeked.TokenType != EExpressionTokenType.Digit && peeked.TokenType != EExpressionTokenType.ParenthesesOpen)
                            {
                                throw new ParseException(EExceptionReason.StartExpressionError, token.Data, token.StartIndex);

                            }
                        }

                        break;
                    case EExpressionTokenType.ParenthesesClose: // next token must be an operator or end or paranthesis close
                        _parenthesesCounter--;
                        if (_parenthesesCounter < 0)
                        {
                            throw new ParseException(EExceptionReason.UnmatchedError, token.Data, token.StartIndex); // unmatched )
                        }
                        {
                            var peeked = tokenStream.Peek();
                            if (peeked.TokenType != EExpressionTokenType.Div && peeked.TokenType != EExpressionTokenType.Mult &&
                                peeked.TokenType != EExpressionTokenType.Add && peeked.TokenType != EExpressionTokenType.Sub &&
                                peeked.TokenType != EExpressionTokenType.End && peeked.TokenType != EExpressionTokenType.ParenthesesClose)
                            {
                                throw new InvalidTokenException(peeked.Data, token.StartIndex, token.Data);
                            }
                        }
                        break;
                    case EExpressionTokenType.Digit: // next token must be parantheses close or an operation or end
                        {
                            var peeked = tokenStream.Peek();

                            if (peeked.TokenType != EExpressionTokenType.Add &&
                                peeked.TokenType != EExpressionTokenType.Sub &&
                                peeked.TokenType != EExpressionTokenType.Mult &&
                                peeked.TokenType != EExpressionTokenType.Div &&
                                peeked.TokenType != EExpressionTokenType.ParenthesesClose &&
                                peeked.TokenType != EExpressionTokenType.End)
                            {
                                throw new InvalidTokenException(peeked.Data, token.StartIndex, token.Data);
                            }
                        }
                        break;

                    case EExpressionTokenType.Add: // next token must be parantheses or digit
                    case EExpressionTokenType.Sub:
                    case EExpressionTokenType.Mult:
                    case EExpressionTokenType.Div:
                        {
                            var peeked = tokenStream.Peek();
                            if (
                                peeked.TokenType != EExpressionTokenType.ParenthesesOpen &&
                                peeked.TokenType != EExpressionTokenType.Digit)
                            {
                                throw new ParseException(EExceptionReason.UnmatchedError, token.Data, token.StartIndex);
                            }
                        }
                        break;
                }
            }

            if (_parenthesesCounter != 0)
            {
                throw new ParseException(EExceptionReason.UnmatchedError, "(", input.Length); // unclosed parantheses
            }

            return true;
        }
    }

}
