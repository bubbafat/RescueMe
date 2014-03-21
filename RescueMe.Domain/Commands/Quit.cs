using RescueMe.Data;
using System.Linq;

namespace RescueMe.Domain.Commands
{
    class Quit: ICommand
    {
        public string Name
        {
            get { return "quit"; }
        }

        public string Execute(IncomingSmsMessage message)
        {
            using (IDataStore store = DataStore.GetInstance())
            {
                if (store.Registrations.Any(r => r.Number == message.From))
                {
                    store.CancelAccount(message.From);
                    
                    return "You have been removed";
                }

                return null;
            }
        }
    }
}
