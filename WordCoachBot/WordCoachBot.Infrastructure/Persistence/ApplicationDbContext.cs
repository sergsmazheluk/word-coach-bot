using Microsoft.EntityFrameworkCore;
using WordCoachBot.Domain;

namespace WordCoachBot.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Word> Words => Set<Word>();
    }
}
