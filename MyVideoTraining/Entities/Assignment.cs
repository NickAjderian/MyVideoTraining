using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyVideoTraining.Entities
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public int? AssignmentTypeId { get; set; }
        public int? PersonId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        [MaxLength(255)]
        public string Result { get; set; }

        public virtual AssignmentType AssignmentType { get; set; }
        public virtual Person Person { get; set; }


    }
}