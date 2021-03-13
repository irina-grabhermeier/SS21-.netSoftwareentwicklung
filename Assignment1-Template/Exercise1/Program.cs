using System;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {

            var phonebook = new Phonebook("Dornbirn");

            while (true)
            {
                Console.WriteLine("Write the name you are looking for: ");
                string nameInput = Console.ReadLine();
                if (nameInput == "quit")
                {
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
                if (phoneInput == "quit")
                {
                    Environment.Exit(0);
                }
                else if (phoneInput == "cancel")
                {
                    Console.WriteLine("Adding entry canceled.");
                }
                else
                {
                    phonebook.AddEntry(nameInput, phoneInput);

                    Console.WriteLine("New entry saved.");
                }
            }
            else
            {
                Console.WriteLine(result);
            }
        }
    }
}
