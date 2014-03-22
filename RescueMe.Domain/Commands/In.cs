using System;

namespace RescueMe.Domain.Commands
{
    public class In : ICommand
    {

        public string Name
        {
            get { return "in"; }
        }

        public string Execute(IncomingSmsMessage message)
        {
            const string oops = "Oops!  The format should be \'in 30 minutes do " 
                              + "something\'.  You can use 'seconds', 'minutes', or 'hours'";
            
            // "in" is already parsed out
            // 30 minutes <do something>
            string[] args = message.Content.Split(new [] {' '}, 3, StringSplitOptions.RemoveEmptyEntries);

            if (args.Length != 3)
            {
                return oops;
            }

            // the number of units (seconds, minutes, hours)
            int length;
            if (!int.TryParse(args[0], out length))
            {
                return oops;
            }

            // we're converting the unit to seconds
            int multiple;
            switch (args[1].ToLowerInvariant())
            {
                case "s":
                case "sec":
                case "secs":
                case "second":
                case "seconds":
                    multiple = 1;
                    break;
                case "m":
                case "min":
                case "mins":
                case "minute":
                case "minutes":
                    multiple = 60;
                    break;
                case "h":
                case "hrs":
                case "hour":
                case "hours":
                    multiple = 60*60;
                    break;
                default:
                    return oops;
            }

            string content = args[2].Trim();

            // there has to be some content
            if (string.IsNullOrEmpty(content))
            {
                return oops;
            }

            // when the message will be sent
            DateTimeOffset when = DateTimeOffset.UtcNow.AddSeconds(length*multiple);

            // Queue the message
            OutboundSmsMessageQueue.Add(
                new OutboundSmsMessage
                {
                    From = Twilio.Config.From,
                    To = message.From,
                    Body = content,
                    ScheduledTime = when,
                });

            return "Your RescueMe message has been queued!";
        }
    }
}
