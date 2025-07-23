using Microsoft.AspNetCore.Mvc;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;



[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly BarangayContext _context;
    private readonly AuthService _auth;

    public AuthController(BarangayContext context, AuthService auth)
    {
        _context = context;
        _auth = auth;
    }

    [HttpPost("register")]
    public IActionResult Register(UserDto request)
    {
        if (_context.Users.Any(u => u.Username == request.Username))
            return BadRequest("User already exists.");

        _auth.CreatePasswordHash(request.Password, out byte[] hash, out byte[] salt);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = request.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User created.");
    }

    [HttpPost("login")]
    public IActionResult Login(UserDto request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
        if (user == null || !_auth.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            return Unauthorized("Invalid credentials.");

        var token = _auth.CreateToken(user);
        return Ok(new { token });
    }
}

public class UserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "Resident";
}