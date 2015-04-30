using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyVideoTraining.Entities
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        public int ClientID { get; set; }
        public int NHSNumber { get; set; }

        [MaxLength(100)]
        public string PersonName { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string NokName { get; set; }

        [MaxLength(100)]
        public string Mobile { get; set; }

        public virtual IList<Assignment> Assignments { get; set; }


    }
}