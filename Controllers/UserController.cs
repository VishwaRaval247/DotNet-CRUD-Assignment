using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET: api/users
    [HttpGet]
    public ActionResult<List<User>> GetUsers() => Ok(_userRepository.GetAllUsers());

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _userRepository.GetUserById(id);
        return user != null ? Ok(user) : NotFound(new { error = "User not found" });
    }

    // POST: api/users
    [HttpPost]
    public ActionResult<User> AddUser([FromBody] User user)
    {
        if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
            return BadRequest(new { error = "Invalid user data" });

        _userRepository.AddUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (_userRepository.GetUserById(id) == null)
            return NotFound(new { error = "User not found" });

        user.Id = id;
        _userRepository.UpdateUser(user);
        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        if (_userRepository.GetUserById(id) == null)
            return NotFound(new { error = "User not found" });

        _userRepository.DeleteUser(id);
        return NoContent();
    }
}
