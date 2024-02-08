using Microsoft.EntityFrameworkCore;
using TechChallengue.Models;

namespace TechChallengue.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<Player> Player { get; set; }
        public DbSet<Move> Moves { get; set; }

        public DbSet<ScoreHistory> ScoreHistories { get; set; }
        public DbSet<Winner> Winner { get; set; }

    }
}
