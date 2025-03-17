using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PettsController : ControllerBase
    {
        // GET: api/<PettsControler>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "cat", "dog" };
        }

        // GET api/<PettsControler>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PettsControler>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<PettsControler>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<PettsControler>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
