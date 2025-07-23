using Microsoft.AspNetCore.Mvc;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly BarangayContext _context;

    public AnnouncementController(BarangayContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Announcements.ToList());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create([FromBody] Announcement announcement)
    {
        _context.Announcements.Add(announcement);
        _context.SaveChanges();
        return Ok(announcement);
    }
}
