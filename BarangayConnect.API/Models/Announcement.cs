namespace BarangayConnect.API.Models;
public class Announcement
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PostedOn { get; set; } = DateTime.UtcNow;
}