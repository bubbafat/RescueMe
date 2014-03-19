using RescueMe.Commands;
using System.Net;
using System.Web.Mvc;
using Twilio.Mvc;
using Twilio.TwiML.Mvc;

namespace RescueMe.Web.Controllers
{
    public class SmsController : TwilioController
    {

        [HttpPost]
        //[ValidateRequest(RescueMe.Twilio.Config.AuthKey)]
        public ActionResult Index(SmsRequest request)
        {
            CommandProcessor p = new CommandProcessor();
            p.Process(request.From, request.Body);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}