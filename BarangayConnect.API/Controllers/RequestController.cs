using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class RequestController : ControllerBase
{
    private readonly BarangayContext _context;

    public RequestController(BarangayContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllRequests()
    {
        var requests = _context.Requests.ToList();
        return Ok(requests);
    }

    [Authorize(Roles = "Resident,Admin")]
    [HttpPost]
    public IActionResult CreateRequest([FromBody] Request request)
    {
        _context.Requests.Add(request);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAllRequests), new { id = request.Id }, request);
    }
}
