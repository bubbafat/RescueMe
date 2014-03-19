using RescueMe.Twilio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Commands.Commands
{
    public class Help : ICommand
    {
        public string Name
        {
            get { return "help"; }
        }

        public void Execute(IncomingSmsMessage message)
        {
            OutgoingSms help = new OutgoingSms
            {
                To = message.From,
                Content = "Replay with \'in 30 seconds Hey, I think something is wrong with your dog.  Please come home ASAP!\'",
            };

            OutgoingSmsQueue.Send(help);
        }
    }
}
