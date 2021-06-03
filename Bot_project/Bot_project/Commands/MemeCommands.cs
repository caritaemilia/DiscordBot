using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bot_project.Commands
{
    public class MemeCommands : BaseCommandModule
    {
        [Command("dailymeme")]
        [Description("Get your daily programming meme by writing ?dailymeme")]
        public async Task DailyMeme(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://www.reddit.com/r/ProgrammerHumor/random.json?limit=1");
            JArray arr = JArray.Parse(result);
           JObject post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());

            var builder = new DiscordEmbedBuilder()
                .WithImageUrl(post["url"].ToString())
                .WithColor(new DiscordColor(33, 176, 252))
                .WithTitle(post["title"].ToString())
                .WithUrl("https://reddit.com" + post["permalink"].ToString());
            var embed = builder.Build();
            await ctx.Channel.SendMessageAsync(embed);

        }



        [Command("othermeme")]
        [Description("Get your random meme by writing ?othermeme")]
        public async Task OtherMeme(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://www.reddit.com/r/Memes/random.json?limit=1");
            JArray arr = JArray.Parse(result);
            JObject post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());

            var builder = new DiscordEmbedBuilder()
                .WithImageUrl(post["url"].ToString())
                .WithColor(new DiscordColor(33, 176, 252))
                .WithTitle(post["title"].ToString())
                .WithUrl("https://reddit.com" + post["permalink"].ToString());
            var embed = builder.Build();
            await ctx.Channel.SendMessageAsync(embed);

        }


    }
}
