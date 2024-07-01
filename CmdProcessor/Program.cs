using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandApp04
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a command:");
            string command = Console.ReadLine();

            var handler = new CommandHandler();
            var commandProcessor = new CommandProcessor(handler);

            Console.WriteLine($"Processing command: {commandProcessor.ProcessCommand(command)}");

            Console.ReadLine(); //just to don't close the window
        }


    }

    public interface ICommandHandler
    {
        string Handle(string command);
    }
    public class CommandHandler : ICommandHandler
        {
            public string Handle(string command)
            {
                if (string.IsNullOrWhiteSpace(command))
                {
                    return "Invalid command";
                }

                switch (command.ToLower())
                {
                    case "start":
                        return "Command 'start' executed";
                    case "stop":
                        return "Command 'stop' executed";
                    case "pause":
                        return "Command 'pause' executed";
                    default:
                        return "Unknown command";
                }
            }
        }

        public class CommandProcessor
        {
            private readonly ICommandHandler _commandHandler;

            public CommandProcessor(ICommandHandler commandHandler)
            {
                _commandHandler = commandHandler;
            }

            public string ProcessCommand(string command)
            {
                return _commandHandler.Handle(command);
            }
        }
    
}
