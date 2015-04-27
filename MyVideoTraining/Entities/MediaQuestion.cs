using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyVideoTraining.Entities
{
    public class MediaQuestion
    {
        [Key]
        public int MediaQuestionId { get; set; }
        public int? MediaId { get; set; }
        public int? Sequence { get; set; }
        [MaxLength(255)]
        public string Question { get; set; }
        [MaxLength(1024)]
        public string Responses { get; set; }

        public virtual Media Media { get; set; }
    }
}