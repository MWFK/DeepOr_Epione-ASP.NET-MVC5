using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class Used_Timeslot_Rate_WeekController : ApiController
    {
        IUsed_Timeslot_Rate_Service X = new Used_Timeslot_Rate_Service();

        //dbset GetAll() problem, was that WebAPI cannot find the db
        //the services works in EpioneMVC, because it's web.config has the same connectionstring as Console App.config, 
        //whereas WebApi dosent, so when this last one try to use the service, he tries to access the DB from his mal configured web.config

        // GET: api/Used_Timeslot_Rate_Week
        public float Get()
        {
            return X.Timeslot_Rate_Per_Week();
        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
