using RescueMe.Commands.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Commands
{
    public class CommandProcessor
    {
        List<ICommand> _commands = new List<ICommand>
        {
            new Help(),
            new Quit(),
            new Register(),
        };

        public void Process(string from, string message)
        {
            IncomingSmsMessage sms;
            if(TryCrackMessage(from, message, out sms))
            {
                FindCommand(sms).Execute(sms);
            }
        }

        private ICommand FindCommand(IncomingSmsMessage sms)
        {
            ICommand found = _commands.FirstOrDefault(c => c.Name.Equals(sms.Command, StringComparison.InvariantCultureIgnoreCase));
            if(found == null)
            {
                found = new Help();
            }

            return found;
        }
        private bool TryCrackMessage(string from, string message, out IncomingSmsMessage sms)
        {
            string[] split = message.Trim().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            string command;
            string content;

            if (split != null || split.Length == 0)
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
