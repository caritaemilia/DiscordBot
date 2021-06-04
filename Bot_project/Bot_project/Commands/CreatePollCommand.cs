using Bot_project.Handlers;
using Bot_project.Handlers.Dialogue;
using BotCore.Services;
using DiscordBotDatabase.Models.cs;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Bot_project.Commands
{
    public class CreatePollCommand : BaseCommandModule
    {
        private readonly IPollService _pollService;

        public CreatePollCommand(IPollService pollService)
        {
            _pollService = pollService;
        }


        [Command("createpoll")]
        [Description("Create a poll")]
        public async Task CreatePoll(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync().ConfigureAwait(false);

            var pollOptions = new StringStep("What are the poll options, please divide the options with comma", null);

            var pollName = new StringStep("What is the poll called?", pollOptions);


            var poll = new Poll();

            pollName.OnValidResult += (result) => poll.PollName = result;


            pollOptions.OnValidResult += (result) => poll.choices = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);


            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                userChannel,
                ctx.User,
                pollName

                );


            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);
            if (!succeeded) { return; }

            await _pollService.CreateNewPollAsync(poll).ConfigureAwait(false);


            await ctx.Member.SendMessageAsync($"Poll: {poll.PollName} Created Successfully").ConfigureAwait(false);

            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = "#" + poll.Id + " " + poll.PollName.ToUpper(),
                Color = DiscordColor.Red,
                Description = string.Join(" ", poll.choices).Replace(',', '\n')
            };


            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

            await pollMessage.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":thumbsup:"));


        }

    }
}