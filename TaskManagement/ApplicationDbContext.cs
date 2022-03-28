using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AppTask> Tasks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
