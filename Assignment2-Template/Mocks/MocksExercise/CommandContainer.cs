using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    public class CommandContainer
    {
        /// <summary>
        /// Florian Eckhart, Irina Grabher Meier
        /// </summary>
        private List<ICommand> _commands = new List<ICommand>();

        public CommandContainer(IInputReader inputReader)
        { 
            inputReader.InputEntered += InputReader_InputEntered;
        }

        public void AddCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("Command cannot be null.");
            }
            _commands.Add(command);
        }

        private void InputReader_InputEntered(object sender, InputReadEventArgs e)
        {
            // Split input into command and args. 
            //
            string[] split = e.InputData.Split(' ');
            if (split.Length == 0)
            {
                return;
            }
            string cmdInput = split[0];
            var args = split.ToList();
            args.RemoveAt(0); // we checked the length above

            foreach (var command in _commands)
            {
                if(command.Accept(cmdInput, args))
                {
                    try
                    {
                        command.Execute(args);
                    }
                    catch (InvalidCommandInputException ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
            }
        }
    }
}
