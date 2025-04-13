using Microsoft.EntityFrameworkCore;

namespace FormApp3.Server.Models
{
    public class StudentFormDbContext : DbContext
    {
        public StudentFormDbContext(DbContextOptions<StudentFormDbContext> options) : base(options) { }
        public DbSet<StudentForm> StudentsForms { get; set; }
    }
}
