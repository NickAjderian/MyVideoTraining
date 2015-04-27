using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyVideoTraining.Entities
{
    public class AssignmentType
    {
        [Key]
        public int AssignmentTypeId { get; set; }
        [MaxLength(100)]
        public string AssignmentTypeName { get; set; }

        public virtual IList<Media> Medias { get; set; }
        public virtual IList<Assignment> Assignments { get; set; }
    }
}