using BarangayConnect.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Import JwtBearerDefaults
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel web server to listen on specific ports (HTTP and HTTPS)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5161); // Listen for HTTP on port 5161
    options.ListenLocalhost(7067, listenOptions =>
    {
        listenOptions.UseHttps(); // Listen for HTTPS on port 7067
    });
});

// Add services to the dependency injection container
builder.Services.AddControllers(); // Registers controllers for MVC
builder.Services.AddEndpointsApiExplorer(); // Enables endpoint discovery for Swagger/OpenAPI
builder.Services.AddSwaggerGen(); // Registers Swagger generator for API documentation

// Register the database context with SQL Server connection string
builder.Services.AddDbContext<BarangayContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AuthService for dependency injection
builder.Services.AddScoped<AuthService>();

// Configure JWT authentication for the API
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Do not validate the token issuer
            ValidateAudience = false, // Do not validate the token audience
            ValidateLifetime = true, // Validate token expiration
            ValidateIssuerSigningKey = true, // Validate the signing key
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)) // Use secret key from config
        };
    });

builder.Services.AddAuthorization(); // Adds authorization services

var app = builder.Build();

// Enable Swagger UI in development environment for API testing and docs
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

app.UseAuthorization(); // Enables authorization middleware

app.MapControllers();  // Maps controller endpoints to routes

app.UseAuthentication(); // Enables authentication middleware
app.UseAuthorization(); // Enables authorization middleware again (should be after authentication)

// Minimal API endpoint for weather forecast example
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm",
        "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    // Generates a list of random weather forecasts
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();
    return forecast;
})
.WithName("GetWeatherForecast") // Names the endpoint for OpenAPI
.WithOpenApi(); // Includes endpoint in OpenAPI/Swagger docs

app.Run(); // Starts the web application

// Record type for weather forecast data
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    // Computed property to convert Celsius to Fahrenheit
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}