using RescueMe.Data;
using RescueMe.Twilio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Commands.Commands
{
    internal class Register : ICommand
    {
        public string Name
        {
            get { return "register"; }
        }

        public void Execute(IncomingSmsMessage message)
        {
            OutgoingSms reply = new OutgoingSms
            {
                To = message.From
            };

            using (IDataStore store = DataStore.GetInstance())
            {
                if(store.Registrations.Any(r => r.Number == message.From))
                {
                    // the user is already registered
                    reply.Content = "You are already registered.  Text 'help' to get started";
                }
                else
                {
                    // the user is already registered
                    reply.Content = "You are now registered!  Text 'help' to get started";
                }
            }

            OutgoingSmsQueue.Send(reply);
        }
    }
}
