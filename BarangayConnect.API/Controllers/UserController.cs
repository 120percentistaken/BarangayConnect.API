using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly BarangayContext _context;

    public UserController(BarangayContext context)
    {
        _context = context;
    }

    // 🔍 GET: api/user
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _context.Users.Select(u => new {
            u.Id,
            u.Username,
            u.Role
        }).ToList();

        return Ok(users);
    }

    // 🔍 GET: api/user/{id}
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();

        return Ok(new {
            user.Id,
            user.Username,
            user.Role
        });
    }

    // ✏️ PUT: api/user/{id}/role
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/role")]
    public IActionResult UpdateUserRole(int id, [FromBody] string role)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();

        user.Role = role;
        _context.SaveChanges();

        return Ok(new { message = "User role updated successfully." });
    }

    // 🗑️ DELETE: api/user/{id}
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }
}
