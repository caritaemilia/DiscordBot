using Bot_project.Handlers.Dialogue;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;

namespace MyFirstBot.Hendlers.Dialog.Steps
{
    public class IntStep : DialogueStepBase
    {
        private IDialogueStep _nextStep;
        private readonly int? _minValue;


        public IntStep(string content, IDialogueStep nextStep, int? minValue = null) : base(content)
        {
            _nextStep = nextStep;
            _minValue = minValue;
        }

        public Action<int> OnValidResult { get; set; } = delegate { };

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

            if (_minValue.HasValue)
            {
                embedBuilder.AddField("Min value:", $"{_minValue.Value}");
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

                if (!int.TryParse(messageResult.Result.Content, out int inputValue))
                {
                    await TryAgain(channel, $"Your input is not integer").ConfigureAwait(false);
                    continue;
                }

                if (_minValue.HasValue)
                {
                    if (inputValue < _minValue.Value)
                    {
                        await TryAgain(channel, $"Your input value is {inputValue} is smaller than: {_minValue}").ConfigureAwait(false);
                        continue;
                    }
                }
 
                OnValidResult(inputValue);

                return false;
            }
        }

    }
}
