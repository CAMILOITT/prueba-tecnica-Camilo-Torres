using Microsoft.EntityFrameworkCore;
using roulettegame.Models;

namespace roulettegame.Data
{
    public class RouletteGameContext : DbContext
    {
        public RouletteGameContext(DbContextOptions<RouletteGameContext> options)
            : base(options)
        {

        }

        public DbSet<PlayerModel>? Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerModel>(entity => 
            {
                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2);
            });

            modelBuilder.
                Entity<PlayerModel>().HasKey(e => e.Id);
        }
    }
}
