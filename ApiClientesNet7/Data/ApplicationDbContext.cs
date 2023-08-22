using ApiClientesNet7.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiClientesNet7.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Client> clients { get; set; }
        public DbSet<User> users { get; set; }

    }
}
