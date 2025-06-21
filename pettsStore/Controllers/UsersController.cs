using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.Json;
using DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _UserService;
        public UsersController(IUserService userService)
        {
            _UserService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            return await _UserService.getAllUsers();
        }
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            return await _UserService.getUserById(id);
        }
        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserRegisterDTO user)
        {
            try
            {
                UserDTO newUser = await _UserService.addUser(user);
                return newUser;
            }catch(ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
           
            //return CreatedAtAction(nameof(Get), new { id = user.Id }, newUser);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserLoginDTO newUser)
        {
            UserDTO user = await _UserService.login(newUser);
            if(user!= null)
            {
                return Ok(user);
            }
            return NotFound(new { Message = "User not found." });
        }
        [Route("password")]
        [HttpPost]
        public Boolean CheckPasswordStrength([FromBody] string password)
        {
            Boolean strength = _UserService.GetPassStrength(password);
            return strength;
        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UserRegisterDTO userUpdate)
        {
            UserDTO user =await _UserService.updateUser(id, userUpdate);
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