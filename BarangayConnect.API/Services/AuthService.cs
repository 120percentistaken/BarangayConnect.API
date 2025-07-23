using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

public class AuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    // Generates a password hash and salt using HMACSHA512 for secure storage.
    public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key; // Randomly generated key used as salt
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Hashes the password
    }

    // Verifies a password by hashing it with the stored salt and comparing to the stored hash.
    public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt); // Use stored salt as key
        var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Hash input password
        return computed.SequenceEqual(storedHash); // Compare with stored hash
    }

    // Creates a JWT token for the authenticated user with claims for username and role.
    public string CreateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username), // User's username claim
            new Claim(ClaimTypes.Role, user.Role)      // User's role claim
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)); // Secret key from config
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);  // Signing credentials

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12), // Token expiration time
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token); // Serializes token to string
    }
}