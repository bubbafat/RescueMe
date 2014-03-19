using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Data
{
    public class Rescue
    {
        public int Id { get; set; }

        public Registration Who { get; set; }

        public DateTimeOffset When { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }
    }
}
