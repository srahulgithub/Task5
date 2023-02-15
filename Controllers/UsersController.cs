using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UsersContext _context;

    public UsersController(UsersContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        return await _context.GetUsers();
    }

    [HttpGet("{id:length(24)}", Name = "GetUser")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _context.GetUser(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        await _context.CreateUser(user);
        return CreatedAtRoute("GetUser", new { id = user.Id }, user);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User userIn)
    {
        var user = await _context.GetUser(id);

        if (user == null)
        {
            return NotFound();
        }

        await _context.UpdateUser(id, userIn);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _context.GetUser(id);

        if (user == null)
        {
            return NotFound();
        }

        await _context.DeleteUser(id);

        return NoContent();
    }
}
