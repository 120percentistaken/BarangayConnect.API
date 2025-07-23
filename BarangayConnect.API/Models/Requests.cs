namespace BarangayConnect.API.Models;

public class Request
{
    public int Id { get; set; }
    public string ResidentName { get; set; }
    public string Type { get; set; } // Clearance, Certificate, etc.
    public string Status { get; set; } = "Pending";
    public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
}