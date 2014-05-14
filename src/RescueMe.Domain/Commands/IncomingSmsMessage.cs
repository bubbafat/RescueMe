
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
