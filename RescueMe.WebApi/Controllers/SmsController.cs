﻿using RescueMe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [ValidateRequest(RescueMe.Twilio.Config.AuthKey)]
        public IHttpActionResult Post(SmsRequest request)
        {
            TwilioResponse response = new TwilioResponse();
            response.Sms(CommandProcessor.Execute(request.From, request.Body));
            return TwiML(response);
        }
    }
}