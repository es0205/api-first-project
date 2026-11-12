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
        IUsersService _usersService;
        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        // GET: api/<User>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "something";
        }
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            var result = ps.EvaluatePassword(user.password);

            if (result.IsValid == false)
            {
                return BadRequest(result);
            }
                        user=_usersService.AddUser(user);
            return CreatedAtAction(nameof(Get),new { id = user.id },user);

        }
        

        [HttpPost("Login")]

        public ActionResult<UserLog> PostLog([FromBody] UserLog loginUser)
        {
            var user= _usersService.GetUser(loginUser);
            if(user == null)
            {
                return NotFound();
            }

           
            return Ok(user);
        }

        // PUT api/<User>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User userToUpdate)
        {
            var result = ps.EvaluatePassword(userToUpdate.password);

            if (result.IsValid == false)
            {
                return BadRequest(result);
            }
            _usersService.UpdateUser(id, userToUpdate);
            return Ok();
        }

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
