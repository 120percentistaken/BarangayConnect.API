using Microsoft.AspNetCore.Mvc;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;
using System.Linq;
[ApiController]
[Route("api/[controller]")]
public class OfficialContactController : ControllerBase
{
    private readonly BarangayContext _context;

    public OfficialContactController(BarangayContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllContacts()
    {
        return Ok(_context.OfficialContacts.ToList());
    }

    [HttpPost]
    public IActionResult AddContact([FromBody] OfficialContact contact)
    {
        _context.OfficialContacts.Add(contact);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAllContacts), new { id = contact.Id }, contact);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteContact(int id)
    {
        var contact = _context.OfficialContacts.Find(id);
        if (contact == null) return NotFound();
        _context.OfficialContacts.Remove(contact);
        _context.SaveChanges();
        return NoContent();
    }
}
