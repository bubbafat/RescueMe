using System.Web.Mvc;
using Twilio.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace Twilio.Echo.Controllers
{
    public class EchoController : TwilioController
    {
        [HttpPost]
        public ActionResult Index(SmsRequest message)
        {
            TwilioResponse response = new TwilioResponse();
            response.Message(message.Body);
            return TwiML(response);
        }
	}
}