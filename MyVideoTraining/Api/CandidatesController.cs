using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MyVideoTraining.Models;
using MyVideoTraining.Entities;
using MyVideoTraining.Helpers;
using Newtonsoft.Json;

namespace MyVideoTraining.Api
{
    public class CandidatesController : ApiController
    {
        private MVTDataModel db = new MVTDataModel();


        // GET: api/Candidates
        [AllowCrossSiteJson]
        //public async Task<IEnumerable<Person>> GetPeople()
        public string GetPeople()
        {
            var ppl = db.People.Include("Assignments").Cast<Person>().ToListAsync();//;
            //foreach (var person in ppl)
            //{
            //    person.Assignments = db.Assignments.Where(x => x.PersonId == person.PersonId).ToList();
            //}
            return JsonConvert.SerializeObject(ppl,Formatting.None, new JsonSerializerSettings{ReferenceLoopHandling=ReferenceLoopHandling.Ignore} );
        }

        // GET: api/Candidates/5
        [AllowCrossSiteJson]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> GetCandidateViewModel(int id)
        {
            var person = db.People.Include("Assignments").FirstOrDefault(x=>x.PersonId == id);//;
            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CandidateViewModelExists(int id)
        {
            return db.People.Count(e => e.PersonId == id) > 0;
        }
    }
}