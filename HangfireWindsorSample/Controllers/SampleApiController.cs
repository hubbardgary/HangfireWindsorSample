using HangfireWindsorSample.Dtos;
using System.Web.Http;

namespace HangfireWindsorSample.Controllers
{
    public class SampleApiController : ApiController
    {
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            if (id > 0)
            {
                return Ok("good");
            }
            return BadRequest("naughty");
        }

        // POST api/<controller>
        public IHttpActionResult Post(PostApiDto model)
        {
            if (model.Value1 > 0)
            {
                return Ok("good");
            }
            return BadRequest("naughty");
        }
    }
}