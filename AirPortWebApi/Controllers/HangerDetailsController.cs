using AirPortWebApi.Models.DataLayer;
using AirPortWebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AirPortWebApi.Controllers
{
    [EnableCors("*","*","*")]
    public class HangerDetailsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult AddHanger([FromBody] Hanger h)
        {
            HangerDetailsDbOperations Hd = new HangerDetailsDbOperations();
            string s = Hd.AddHanger(h);
            List<string> list = s.Split(',').ToList();
            if (list[0].Equals("0"))
            {
                return Ok(list[1]);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, list[1]);
            }
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}