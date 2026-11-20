
using Microsoft.EntityFrameworkCore;
using russelregadillo.Models;

namespace russelregadillo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
