using DiscordBotDatabase.Models.cs;
using System.Threading.Tasks;

namespace BotCore.Services
{
    public interface IPollService

    {
        Task CreateNewPollAsync(Poll poll);
        Task<Poll> GetPollByNameAsync(string Pollname);

        Task CreateNewVoteAsync(Vote vote);
    }
}
