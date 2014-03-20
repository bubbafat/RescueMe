using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Domain.Commands
{
    public class IncomingSmsMessage
    {
        public IncomingSmsMessage(string from, string command, string content)
        {
            From = from;
            Command = command;
            Content = content;
        }

        public string From { get; private set; }

        public string Command { get; private set; }

        public string Content { get; private set; }
    }
}
