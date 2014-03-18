using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace RescueMe.Web.Controllers
{
    public class SmsController : TwilioController
    {

        [HttpPost]
        [ValidateRequest("ed3c72f9cef57579ca7a2b7f0ea61577")]
        public ActionResult Index(SmsRequest request)
        {
            TwilioResponse response = new TwilioResponse();
            return TwiML(response.Sms(request.Body.Trim()));
        }
	}
}