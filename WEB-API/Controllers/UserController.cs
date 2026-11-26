using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Text.Json;
using Zxcvbn;



namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        PasswordsService ps = new PasswordsService();
        private readonly ILogger<UserController> _logger;

        private readonly IUsersService _usersService;
        public UserController(IUsersService usersService, ILogger<UserController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }
        // GET: api/<User>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        //Why does GET by id return a fixed text? The assignment asks it to read the user with this id from the file (or NotFound / NoContent)
        public string Get(int id)
        {
            return "something";
        }
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            var result = ps.EvaluatePassword(user.Password);

            if (result.IsValid == false)
            {
                return BadRequest(result);
            }
                        await _usersService.AddUser(user);

            return CreatedAtAction(nameof(Get),new { id = user.Id },user);

        }
        

        [HttpPost("Login")]

        public async Task<ActionResult<UserLog>> PostLog([FromBody] UserLog loginUser)
        {
            var user= await _usersService.GetUser(loginUser);
            if(user == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"User {user.UserName} logged in successfully. password:{loginUser.password}, first name:{user.FirstName}, last name:{user.LastName}");
            return Ok(user);
        }

        // PUT api/<User>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User userToUpdate)
        {
            var result = ps.EvaluatePassword(userToUpdate.Password);

            if (result.IsValid == false)
            {
                return BadRequest(result);
            }
            await _usersService.UpdateUser(id, userToUpdate);
            return Ok();
        }

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
