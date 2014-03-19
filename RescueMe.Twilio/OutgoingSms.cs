using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Twilio
{
    public sealed class OutgoingSms
    {
        public string To { get; set; }
        public string Content { get; set; }
    }
}
