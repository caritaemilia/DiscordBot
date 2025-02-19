﻿using Bot_project.Handlers.Dialogue;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;

namespace Bot_project.Handlers
{
    public class StringStep : DialogueStepBase
    {

        private readonly int? _minLength;
        private readonly int? _maxLength;
        private IDialogueStep _nextStep;

        public StringStep(string content, IDialogueStep nextStep, int? minLength = null, int? maxLength = null) : base(content)
        {
            _nextStep = nextStep;
            _maxLength = maxLength;
            _minLength = minLength;
        }

        public Action<string> OnValidResult { get; set; } = delegate { };

        public override IDialogueStep NextStep => _nextStep;

        public void SetNextSetp(IDialogueStep nextStep)
        {
            _nextStep = nextStep;
        }

        public override async Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user)
        {
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = $"Please Respond below",
                Description = $"{user.Mention}, {_content}",
            };

            embedBuilder.AddField("To Stop The Dialogue", "Use The ?cancel command");

            if (_minLength.HasValue)
            {
                embedBuilder.AddField("Min length:", $"{_minLength.Value} characters");
            }
            if (_maxLength.HasValue)
            {
                embedBuilder.AddField("Max length:", $"{_maxLength.Value} characters");
            }

            var interactivity = client.GetInteractivity();
            while (true)
            {
                var embed = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

                OnMessageAdded(embed);

                var messageResult = await interactivity.WaitForMessageAsync(x => x.ChannelId == channel.Id && x.Author.Id == user.Id).ConfigureAwait(false);


                OnMessageAdded(messageResult.Result);

                if (messageResult.Result.Content.Equals("?cancel", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (_minLength.HasValue)
                {
                    if (messageResult.Result.Content.Length < _minLength.Value)
                    {
                        await TryAgain(channel, $"Your input is {_minLength.Value - messageResult.Result.Content.Length} characters too short").ConfigureAwait(false);
                        continue;
                    }
                }
                if (_maxLength.HasValue)
                {
                    if (messageResult.Result.Content.Length > _maxLength.Value)
                    {
                        await TryAgain(channel, $"Your input is {messageResult.Result.Content.Length - _maxLength.Value} characters too long").ConfigureAwait(false);
                        continue;
                    }
                }

                OnValidResult(messageResult.Result.Content);
                return false;
            }
        }

    }
}
