using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyVideoTraining.Entities
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }
        public int? AssignmentTypeId { get; set; }
        [MaxLength(100)]
        public string MediaName { get; set; }
        [MaxLength(255)]
        public string Url { get; set; }

        public virtual IList<MediaQuestion> Questions { get; set; }
        public virtual AssignmentType AssignmentType { get; set; }
    }
}