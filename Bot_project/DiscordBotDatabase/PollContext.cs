using DiscordBotDatabase.Models.cs;
using Microsoft.EntityFrameworkCore;

namespace DiscordBotDatabase
{
    public class PollContext : DbContext
    {
        public PollContext(DbContextOptions<PollContext> options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }

        public DbSet<Vote> Votes { get; set; }

    }
}
