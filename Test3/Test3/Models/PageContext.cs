using Microsoft.EntityFrameworkCore;

namespace Test3.Models
{
    public class PageContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public PageContext(DbContextOptions<PageContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
