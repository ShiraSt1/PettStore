using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserService userService = new UserService();

        private static readonly List<User> users = new List<User>();
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            User newUser = userService.addUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.userId }, newUser);
        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] User newUser)
        {
            User user = userService.login(newUser);
            if(user!= null)
            {
                return Ok(user);
            }
            return NotFound(new { Message = "User not found." });
        }

        [Route("password")]
        [HttpPost]
        public ActionResult<User> CheckPasswordStrength([FromBody] string password)
        {
            int strength = userService.GetPassStrength(password);
            return Ok(strength);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody]User userUpdate)
        {
            User user=userService.updateUser(id, userUpdate);
            if(user != null)
            {
                return Ok(user);
            }
            return NotFound(new { Message = "User not found." });
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
