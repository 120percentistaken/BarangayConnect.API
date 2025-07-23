using Microsoft.AspNetCore.Mvc;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly BarangayContext _context;

    public ComplaintController(BarangayContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Complaints.ToList());
    }

    [Authorize(Roles = "Resident,Admin")]
    [HttpPost]
    public IActionResult Create([FromBody] Complaints complaint)
    {
        _context.Complaints.Add(complaint);
        _context.SaveChanges();
        return Ok(complaint);
    }
}
