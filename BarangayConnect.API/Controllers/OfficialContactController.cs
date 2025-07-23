using Microsoft.AspNetCore.Mvc;
using BarangayConnect.API.Models;
using BarangayConnect.API.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
[ApiController]
[Route("api/[controller]")]
public class OfficialContactController : ControllerBase
{
    private readonly BarangayContext _context;

    public OfficialContactController(BarangayContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.OfficialContacts.ToList());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create([FromBody] OfficialContact contact)
    {
        _context.OfficialContacts.Add(contact);
        _context.SaveChanges();
        return Ok(contact);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] OfficialContact updated)
    {
        var contact = _context.OfficialContacts.Find(id);
        if (contact == null) return NotFound();

        contact.Name = updated.Name;
        contact.Position = updated.Position;
        contact.ContactInfo = updated.ContactInfo;

        _context.SaveChanges();
        return Ok(contact);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var contact = _context.OfficialContacts.Find(id);
        if (contact == null) return NotFound();

        _context.OfficialContacts.Remove(contact);
        _context.SaveChanges();
        return NoContent();
    }
}
