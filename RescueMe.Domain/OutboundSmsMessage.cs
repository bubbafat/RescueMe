using Newtonsoft.Json;
using System;

namespace RescueMe.Domain
{
    public class OutboundSmsMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public DateTimeOffset ScheduledTime { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static OutboundSmsMessage FromJson(string json)
        {
            return JsonConvert.DeserializeObject<OutboundSmsMessage>(json);
        }
    }
}
