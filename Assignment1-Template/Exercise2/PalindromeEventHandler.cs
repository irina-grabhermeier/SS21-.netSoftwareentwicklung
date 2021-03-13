using System;

namespace Exercise2
{
    class PalindromeEventHandler : IInputEventHandler
    {
        public event EventHandler<string> InputEntered = delegate { };
        public PalindromeEventHandler()
        {

        }

        public void StartInputLoop()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (PalindromeChecker.Check(input))
                {
                    Console.WriteLine("Palindrome detected.");
                    InputEntered(this, input);
                }
                else
                {
                    Console.WriteLine("No Palindrome.");
                }
            }
        }
    }
}
