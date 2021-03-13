using System.Collections.Generic;

namespace Exercise1
{
    class Phonebook
    {
        private Dictionary<string, string> _phonebook;
        private string _name;
        public Phonebook(string name)
        {
            _name = name;
            _phonebook = new Dictionary<string, string>();
        }

        public string GetPhoneNumber(string name)
        {
            if (_phonebook.ContainsKey(name))
            {
                return _phonebook[name];
            }
            else
            {
                return "invalid";
            }
        }

        public void AddEntry(string name, string phonenumber)
        {
            _phonebook.Add(name, phonenumber);
        }
    }
}
