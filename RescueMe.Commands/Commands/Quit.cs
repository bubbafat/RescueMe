using RescueMe.Data;
using RescueMe.Twilio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Commands.Commands
{
    class Quit: ICommand
    {
        public string Name
        {
            get { return "quit"; }
        }

        public void Execute(IncomingSmsMessage message)
        {

            using (IDataStore store = DataStore.GetInstance())
            {
                if(store.Registrations.Any(r => r.Number == message.From))
                {
                    store.CancelAccount(message.From);

                    OutgoingSms reply = new OutgoingSms
                    {
                        To = message.From,
                        Content = "You have been removed",
                    };

                    OutgoingSmsQueue.Send(reply);
                }
            }
        }
    }
}
