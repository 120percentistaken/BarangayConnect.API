using Microsoft.AspNetCore.Mvc;
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
        //return Ok(_context.Requests.ToList());
        var requests = _context.Requests.ToList();
        return Ok(requests);

    }

    [HttpPost]
    public IActionResult CreateRequest([FromBody] Request request)
    {
        _context.Requests.Add(request);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAllRequests), new { id = request.Id }, request);
    }
}