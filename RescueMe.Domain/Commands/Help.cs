﻿namespace RescueMe.Domain.Commands
{
    public class Help : ICommand
    {
        public string Name
        {
            get { return "help"; }
        }

        public string Execute(IncomingSmsMessage message)
        {
            return "Reply with \'in 30 seconds Hey, I think something is wrong with your dog.  Please come home ASAP!\'";
        }
    }
}
