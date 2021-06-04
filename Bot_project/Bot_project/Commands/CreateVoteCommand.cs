using Bot_project.Handlers;
using Bot_project.Handlers.Dialogue;
using BotCore.Services;
using DiscordBotDatabase.Models.cs;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MyFirstBot.Hendlers.Dialog.Steps;
using System.Threading.Tasks;


namespace Bot_project.Commands
{
    public class CreateVoteCommand : BaseCommandModule
    {
        private readonly IPollService _pollService;

        public CreateVoteCommand(IPollService pollService)
        {
            _pollService = pollService;
        }


        [Command("vote")]
        [Description("Vote poll by id")]
        public async Task VotePoll(CommandContext ctx, params string[] stringOptions)
        {
            await ctx.Message.DeleteAsync().ConfigureAwait(false);

            var pollVoteStep = new StringStep("Please give the vote", null);

            var pollIdStep = new IntStep("Please give the poll Id that you're wishing to vote", pollVoteStep);

            var Vote = new Vote();


            string voteName = string.Empty;

            pollIdStep.OnValidResult += (result) => Vote.PollId = result;

            Vote.VoteCount = 1;

            pollVoteStep.OnValidResult += (result) => Vote.VoteName = result;

     
          

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);


            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                userChannel,
                ctx.User,
                pollIdStep

                );
            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);
            if (!succeeded) { return; }

            if (Vote.VoteName == pollVoteStep.ToString())
            {
                Vote.VoteCount++;
                return;
            }
            else
            {
                await _pollService.CreateNewVoteAsync(Vote).ConfigureAwait(false);
            }
          
            await ctx.Member.SendMessageAsync($"Voted for {Vote.VoteName} Successfully!").ConfigureAwait(false);


        }
    }
}
