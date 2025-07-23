using Microsoft.AspNetCore.Mvc;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;
using System.Linq;
[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly BarangayContext _context;

    public ComplaintController(BarangayContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllComplaints()
    {
        return Ok(_context.Complaints.ToList());
    }

    [HttpPost]
    public IActionResult SubmitComplaint([FromBody] Complaints complaint)
    {
        _context.Complaints.Add(complaint);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAllComplaints), new { id = complaint.Id }, complaint);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteComplaint(int id)
    {
        var complaint = _context.Complaints.Find(id);
        if (complaint == null) return NotFound();
        _context.Complaints.Remove(complaint);
        _context.SaveChanges();
        return NoContent();
    }
}
