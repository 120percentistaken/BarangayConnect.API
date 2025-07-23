using Microsoft.AspNetCore.Mvc;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;
using System.Linq;
[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly BarangayContext _context;

    public AnnouncementController(BarangayContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllAnnouncements()
    {
        return Ok(_context.Announcements.OrderByDescending(a => a.PostedOn).ToList());
    }

    [HttpPost]
    public IActionResult CreateAnnouncement([FromBody] Announcement announcement)
    {
        _context.Announcements.Add(announcement);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAllAnnouncements), new { id = announcement.Id }, announcement);
    }
}
