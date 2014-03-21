using RescueMe.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RescueMe.Domain
{
    public class CommandProcessor
    {
        readonly static List<ICommand> _commands = new List<ICommand>
        {
            new Help(),
            new Quit(),
            new Register(),
            new In(),
        };

        public static string Execute(string from, string message)
        {
            IncomingSmsMessage sms;
            if(TryCrackMessage(from, message, out sms))
            {
                return FindCommand(sms).Execute(sms);
            }

            return "Text \'help\' to learn how to use RescueMe.";
        }

        private static ICommand FindCommand(IncomingSmsMessage sms)
        {
            ICommand found = _commands.FirstOrDefault(c => c.Name.Equals(sms.Command, StringComparison.InvariantCultureIgnoreCase));
            if(found == null)
            {
                found = new Help();
            }

            return found;
        }
        private static bool TryCrackMessage(string from, string message, out IncomingSmsMessage sms)
        {
            string[] split = message.Trim().Split(new [] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            string command;
            string content;

            if (split.Length == 0)
            {
                sms = null;
                return false;
            }

            command = split[0];
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
