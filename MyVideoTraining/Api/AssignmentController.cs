using MyVideoTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyVideoTraining.Api
{
    public class AssignmentController : ApiController
    {
        private MVTDataModel db = new MVTDataModel();

        // GET: api/Assignment
        public string Get()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(db.Assignments.ToList());
        }

        // GET: api/Assignment/5
        public string Get(int id)
        {
            var obj = db.Assignments.FirstOrDefault(x => x.AssignmentId == id);
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        // POST: api/Assignment
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Assignment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Assignment/5
        public void Delete(int id)
        {
        }
    }
}
