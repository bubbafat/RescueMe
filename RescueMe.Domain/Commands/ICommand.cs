using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Domain.Commands
{
    internal interface ICommand
    {
        string Name { get; }
        string Execute(IncomingSmsMessage message);
    }
}
