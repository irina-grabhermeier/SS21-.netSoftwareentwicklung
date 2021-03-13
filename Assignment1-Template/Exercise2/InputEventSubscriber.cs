using System;
using System.Collections.Generic;

namespace Exercise2
{

    class InputEventSubscriber
    {
        private IInputEventHandler _inputEventHandler;
        private HashSet<string> _palindromes;
        public InputEventSubscriber(IInputEventHandler inputEventHandler)
        {
            _palindromes = new HashSet<string>();
            _inputEventHandler = inputEventHandler;
            _inputEventHandler.InputEntered += HandleInputEvent;
        }

        private void HandleInputEvent(object sender, string palindrome)
        {
            _palindromes.Add(palindrome);
            PrintPalindromes();
        }

        private void PrintPalindromes()
        {
            Console.WriteLine("List of Palindromes: ");
            foreach (string palindrome in _palindromes)
            {
                Console.WriteLine(palindrome);
            }
        }
    }
}
