using RescueMe.Data;
using System.Linq;

namespace RescueMe.Domain.Commands
{
    public class Help : ICommand
    {
        public string Name
        {
            get { return "help"; }
        }

        public string Execute(IncomingSmsMessage message)
        {
            using (IDataStore store = DataStore.GetInstance())
            {
                if (store.Registrations.Any(r => r.Number == message.From))
                {
                    return "Reply with \'in 30 seconds Hey, I think something is wrong with your dog.  Please come home ASAP!\'";
                }
                else
                {
                    return "Text \'register\' to start using the RescueMe service!";
                }
            }
        }
    }
}
