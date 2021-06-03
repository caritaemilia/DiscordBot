using DiscordBotDatabase;
using DiscordBotDatabase.Models.cs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BotCore.Services
{
    

    public class PollService : IPollService
    {
        
        private readonly DbContextOptions<PollContext> _options;

        public PollService(DbContextOptions<PollContext> options)
        {
            _options = options;
        }

        public async Task CreateNewPollAsync(Poll poll)
        {
            using var context = new PollContext(_options);

            context.Add(poll);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Poll> GetPollByNameAsync(string pollName)
        {
            using var context = new PollContext(_options);

            return await context.Polls.FirstOrDefaultAsync(x => x.PollName.ToLower() == pollName.ToLower()).ConfigureAwait(false);
        }
    }
}
