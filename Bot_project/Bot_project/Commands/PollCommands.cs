﻿using Bot_project.Handlers;
using Bot_project.Handlers.Dialogue;
using BotCore.Services;
using DiscordBotDatabase.Models.cs;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Npgsql;
using System.Threading.Tasks;

namespace Bot_project.Commands
{
    public class PollCommands : BaseCommandModule
    {
        private readonly IPollService _pollService;

        public PollCommands(IPollService pollService)
        {
            _pollService = pollService;
        }

        [Command("pollinfo")]
        [Description("Get info about previously made poll")]
        public async Task PollInfo(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync().ConfigureAwait(false);
            var itemNameStep = new StringStep("Please give the poll name", null);

            string pollName = string.Empty;

            itemNameStep.OnValidResult += (result) => pollName = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                userChannel,
                ctx.User,
                itemNameStep
            );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            Poll poll = await _pollService.GetPollByNameAsync(pollName).ConfigureAwait(false);

            await ctx.Member.SendMessageAsync($"Id: { poll.Id}, Name: {poll.PollName}, Choices: {poll.PollChoices}").ConfigureAwait(false);
        }



        [Command("deletepoll")]
        [Description("Delete poll, this is still in progress")]
        public async Task DeletePoll(CommandContext ctx)
        {

            await ctx.Message.DeleteAsync().ConfigureAwait(false);
            var itemNameStep = new StringStep("Give the pollname that you want to delete", null);

            string nameToDelete = string.Empty;

            itemNameStep.OnValidResult += (result) => nameToDelete = result;
            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                userChannel,
                ctx.User,
                itemNameStep
            );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            NpgsqlConnection conn = new NpgsqlConnection("Server=PostgreSQL 13;Host=localhost;Port=5432;Username=postgres;Password=root;Database=Bot_Project");
            conn.Open();
            string sql = $"DELETE FROM Polls WHERE \"PollName\"={nameToDelete};";
            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();



        }
    }
}
