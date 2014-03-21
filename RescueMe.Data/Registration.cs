using System;
using System.ComponentModel.DataAnnotations;

namespace RescueMe.Data
{
    public class Registration
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Number { get; set; }
        public DateTimeOffset Registered { get; set; }
    }
}
