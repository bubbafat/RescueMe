using RescueMe.Data;
using System.Linq;

namespace RescueMe.Domain.Commands
{
    internal class Register : ICommand
    {
        public string Name
        {
            get { return "register"; }
        }

        public string Execute(IncomingSmsMessage message)
        {
            using (IDataStore store = DataStore.GetInstance())
            {
                if(store.Registrations.Any(r => r.Number == message.From))
                {
                    // the user is already registered
                    return "You are already registered.  Text 'help' to get started";
                }
                else
                {
                    store.Register(message.From);

                    return "You are now registered!  Text 'help' to get started";
                }
            }
        }
    }
}
