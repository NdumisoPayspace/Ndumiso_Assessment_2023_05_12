namespace Ndumiso_Assessment_2023_05_12.Data
{
    using Microsoft.EntityFrameworkCore;

    using Ndumiso_Assessment_2023_05_12.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
