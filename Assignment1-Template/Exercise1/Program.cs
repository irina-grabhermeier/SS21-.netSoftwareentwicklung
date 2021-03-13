using System;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {

            var isRunning = true;
            var phonebook = new Phonebook("Dornbirn");

            while (isRunning)
            {
                Console.WriteLine("Write the name you are looking for: ");
                string nameInput = Console.ReadLine();
                if (nameInput == "quit")
                {
                    isRunning = false;
                    Environment.Exit(0);
                }
                CheckResult(phonebook.GetPhoneNumber(nameInput), phonebook, nameInput);

            }
        }

        private static void CheckResult(string result, Phonebook phonebook, string nameInput)
        {
            if (result == "invalid")
            {
                Console.WriteLine("Please enter your phone number: ");
                var phoneInput = Console.ReadLine();
                phonebook.AddEntry(nameInput, phoneInput);
            }
            else
            {
                Console.WriteLine(result);
            }
        }
    }
}
