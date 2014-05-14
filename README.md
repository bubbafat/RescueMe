RescueMe
========

A simple service that issues "rescue" SMS messages using Twilio.  For my May TriNUG talk on Twilio with .NET

![Useless Architectural Diagram](https://raw.githubusercontent.com/bubbafat/RescueMe/master/ReadMeAssets/TierDiagram.png "Useless Architectural Diagram")

Web Role
=======

The web role is hosting a simple Web API application with a single controller with a single action.  This action is a POST that does this:

    [HttpPost]
    [Route("sms")]
    [ValidateRequest(RescueMe.Twilio.Config.AuthKey)]
    public IHttpActionResult Post(SmsRequest request)
    {
        TwilioResponse response = new TwilioResponse();
        response.Message(CommandProcessor.Execute(request.From, request.Body));
        return TwiML(response);
    }

See?  Super easy.  We process the message and send a TwilioResponse back.  Maybe we send something back (which gets SMS'ed back) or maybe not (empty response).

Also we validate the request.

##But wait - Twilio and Web API don't play well together!

Right - they don't.  But I made a Web API library with a nuget package.  Then I wrote about it [here](http://www.roberthorvick.com/2014/03/20/twilio-web-api-nuget-package-released/).  So if you are using Twilio and WebAPI - maybe check that out.  Or if you work at Twilio ... maybe consider adding it to your .NET client library.

Worker Role
=========

The worker role is pretty brain-dead simple too.  It basically does this:

    public override void Run()
    {
        TwilioRestClient twilio = new TwilioRestClient(Twilio.Config.AccountSid, Twilio.Config.AuthKey);

        while (true)
        {
            OutboundSmsMessageQueue.ProcessNext(sms => twilio.SendSmsMessage(sms.From, sms.To, sms.Body));
        }
    }

So ... in a loop process items from the queue.

*How do things get into the queue?*

The web role puts them there when necessary.

Data (Service Bus Queue)
===============

Scheduled messages are stored in the queue.  That is how they get from the web role to the worker role.

I set the initial visibility delay on the queued item to delay when it can be dequeued until the appropriate time.  This means I don't need to ignore items until the right time.  When they show up - they are good.

I also use a Service Bus queue because they allow blocking reads rather than polling.  This is a much simpler model (and cheaper since reads have a cost in Azure).

The Glue
======

##Commands

### Help

Sends a help message

### In

Where the magic happens.  You can say things like:

"In 30 seconds The cat fell out the window - please come home now!!!"

And in 30 seconds you will get a text saying "The cat fell out the window - please come home now!!!"

You could also use minutes or hours.

## Queue Wrapper

A simple wrapper around the queue to make enqueue/dequeue easier.  It's why the worker role loop is one line long.  It doesn't need to worry about ACK'ing the completed message or deal with blocking, etc.

## Secrets

If you get the code you will notice a missing file: Secrets.cs

This is where my Twilio SID and auth token are.  You don't need to know them.  Add your own.  This is left as an exercise to the reader.










