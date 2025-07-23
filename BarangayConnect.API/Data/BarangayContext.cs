using Microsoft.EntityFrameworkCore;
using BarangayConnect.API.Models;

namespace BarangayConnect.API.Data
{
    public class BarangayContext : DbContext
    {
        public BarangayContext(DbContextOptions<BarangayContext> options) : base(options) { }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Complaints> Complaints { get; set; }
        public DbSet<OfficialContact> OfficialContacts { get; set; }
    }
}