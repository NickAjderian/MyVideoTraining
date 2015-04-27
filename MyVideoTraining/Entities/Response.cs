using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyVideoTraining.Entities
{
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }
        public int? MediaQuestionId { get; set; }
        public int? AssignmentId { get; set; }
        [MaxLength(100)]
        public string ResponseText { get; set; }
        public DateTime? ResponseDate { get; set; }

        public virtual MediaQuestion Question { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}