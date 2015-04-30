using MyVideoTraining.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyVideoTraining.Models
{
    public class CandidateViewModel:Entities.Person
    {
        new public IEnumerable<Assignment> Assignments { get; set; }

    }

}