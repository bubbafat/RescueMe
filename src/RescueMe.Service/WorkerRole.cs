using Microsoft.WindowsAzure.ServiceRuntime;
using RescueMe.Domain;
using System.Net;
using Twilio;

namespace RescueMe.Service
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            TwilioRestClient twilio = new TwilioRestClient(
                Twilio.Config.AccountSid, 
                Twilio.Config.AuthKey);

            while (true)
            {
                OutboundSmsMessageQueue.ProcessNext(sms => 
                    twilio.SendSmsMessage(sms.From, sms.To, sms.Body)
                );
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
