using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Data
{
    public class Registration
    {
        public string Number { get; set; }
        public DateTimeOffset Registered { get; set; }
    }
}
