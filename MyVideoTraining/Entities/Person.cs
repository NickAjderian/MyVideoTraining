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
        [MaxLength(100)]
        public string PersonName { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string Phone { get; set; }

        public virtual IList<Assignment> Assignments { get; set; }


    }
}