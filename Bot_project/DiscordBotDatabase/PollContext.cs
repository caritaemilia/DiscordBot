using DiscordBotDatabase.Models.cs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotDatabase
{
    public class PollContext : DbContext
    {
        public PollContext(DbContextOptions<PollContext> options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }
    }
}
