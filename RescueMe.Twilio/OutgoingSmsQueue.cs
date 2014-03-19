using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace RescueMe.Twilio
{
    public static class OutgoingSmsQueue
    {
        public static void Send(OutgoingSms message)
        {
            TwilioRestClient client = new TwilioRestClient(
                RescueMe.Twilio.Config.AccountSid,
                RescueMe.Twilio.Config.AuthKey);

            client.SendSmsMessage(
                RescueMe.Twilio.Config.From,
                message.To,
                message.Content);
        }
    }
}
