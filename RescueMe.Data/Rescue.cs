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

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(1024)]
        public string Content { get; set; }
    }
}
