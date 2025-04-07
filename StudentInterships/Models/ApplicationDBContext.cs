using Microsoft.EntityFrameworkCore;

namespace StudentInterships.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<Internship> Internships { get; set; }
    }
}
