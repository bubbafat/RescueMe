﻿using RescueMe.Domain;
using System.Web.Http;
using Twilio.TwiML;
using Twilio.TwiML.WebApi;
using Twilio.WebApi;

namespace RescueMe.WebApi.Controllers
{
    public class SmsController : TwilioController
    {
        [HttpPost]
        [Route("sms")]
        [ValidateRequest(Twilio.Config.AuthKey)]
        public IHttpActionResult Post(SmsRequest request)
        {
            string response = CommandProcessor.Execute(request.From, request.Body);

            TwilioResponse twililResponse = new TwilioResponse();

            twililResponse.Message(response);
            
            return TwiML(twililResponse);
        }
    }
}
