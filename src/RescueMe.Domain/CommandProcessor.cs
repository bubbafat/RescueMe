using RescueMe.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RescueMe.Domain
{
    public class CommandProcessor
    {
        readonly static List<ICommand> Commands = new List<ICommand>
        {
            new Help(),
            new In(),
        };

        public static string Execute(string from, string message)
        {
            IncomingSmsMessage sms;

            if(TryCrackMessage(from, message, out sms))
            {
                ICommand command = FindCommand(sms);
                return command.Execute(sms);
            }

            return "Text \'help\' to learn how to use RescueMe.";
        }

        private static ICommand FindCommand(IncomingSmsMessage sms)
        {
            ICommand found = Commands.FirstOrDefault(c => 
                c.Name.Equals(sms.Command, StringComparison.InvariantCultureIgnoreCase));

            return found ?? new Help();
        }

        private static bool TryCrackMessage(string from, string message, out IncomingSmsMessage sms)
        {
            // split the first word out - this is the command.
            // if anything follows, it becomes and argument
            string[] split = message.Trim().Split(new [] { ' ' }, 2, 
                StringSplitOptions.RemoveEmptyEntries);

            string content;

            if (split.Length == 0)
            {
                sms = null;
                return false;
            }

            string command = split[0];

            if(split.Length > 1)
            {
                content = split[1];
            }
            else 
            { 
                content = null; 
            }

            sms = new IncomingSmsMessage(from, command, content);
            return true;
        }
    }
}
