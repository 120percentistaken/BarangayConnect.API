namespace BarangayConnect.API.Models;
public class Complaints
{
    public int Id { get; set; }
    public string ResidentName { get; set; }
    public string Description { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}