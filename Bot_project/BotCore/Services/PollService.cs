using DiscordBotDatabase;
using DiscordBotDatabase.Models.cs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task CreateNewVoteAsync(Vote vote)
        {
            using var context = new PollContext(_options);
            context.Add(vote);
            await context.SaveChangesAsync().ConfigureAwait(false);

        }


        public async Task<Vote> GetVotesById(int pollId)
        {
            using var context = new PollContext(_options);

      
            return await context.Votes.FirstOrDefaultAsync(x => x.PollId == pollId).ConfigureAwait(false);

        }
    }
}
