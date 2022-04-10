using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using LiteDB;
using Newtonsoft.Json;
using SapioxBot.Currency;
using SapioxBot.Database;
using System.Net;

namespace SapioxBot.Commands
{
    public class OtherCommands : ApplicationCommandModule
    {
        [SlashCommand("settings", "test")]
        public async Task Settings(InteractionContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Title = "🦊 SapioxBot Settings",
                Color = DiscordColor.Orange
            };

            var options = new List<DiscordSelectComponentOption>()
            {
                new DiscordSelectComponentOption("SCPSL server inegration", "scpsl"),
                new DiscordSelectComponentOption("Curency", "currency", isDefault: true),
                new DiscordSelectComponentOption("Other", "other")
            };

            var dropdown = new DiscordSelectComponent("settings_menu", null, options);

            var message = new DiscordInteractionResponseBuilder().AddEmbed(embed).AddComponents(dropdown);

            //there should be some code for database but im dumb and idk how to use that yet

            await ctx.CreateResponseAsync(message);
        }

        [SlashCommand("status", "sets bot activity")]
        public async Task Status(InteractionContext ctx, [Option("name", "activity name")] string name)
        {
            await ctx.Client.UpdateStatusAsync(new DiscordActivity() { Name = name, ActivityType = ActivityType.Streaming }, UserStatus.Online);
            await ctx.CreateResponseAsync(new DiscordEmbedBuilder() { Description = $"Bot activity has been set to ``{name}``", Color = DiscordColor.Azure });
        }

        [SlashCommand("help", "help command")]
        public async Task help(InteractionContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Title = "Help",
                Color = DiscordColor.Cyan
            };

            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("catboy", "random catboy picture")]
        public async Task Catboy(InteractionContext ctx)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("https://api.catboys.com/img/catboy");
            string imageurl = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["url"];

            var embed = new DiscordEmbedBuilder()
            {
                Title = "UwU 🐈",
                ImageUrl = imageurl,
                Color = DiscordColor.Cyan
            };

            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("cat", "random cat picture")]
        public async Task cat(InteractionContext ctx)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("https://aws.random.cat/meow");
            string imageurl = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["file"];

            var embed = new DiscordEmbedBuilder()
            {
                Title = "MEOW 🐈",
                ImageUrl = imageurl,
                Color = DiscordColor.Blurple
            };

            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("baka", "random baka gif")]
        public async Task baka(InteractionContext ctx)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("https://api.catboys.com/baka");
            string imageurl = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["url"];

            var embed = new DiscordEmbedBuilder()
            {
                Title = "Baka",
                ImageUrl = imageurl,
                Color = DiscordColor.Cyan
            };

            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("dog", "random dog picture")]
        public async Task dog(InteractionContext ctx)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("https://dog.ceo/api/breeds/image/random");
            string imageurl = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["message"];

            var embed = new DiscordEmbedBuilder()
            {
                Title = "BARK 🐕",
                ImageUrl = imageurl,
                Color = DiscordColor.Blurple
            };

            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("fox", "random fox picture")]
        public async Task fox(InteractionContext ctx)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("https://randomfox.ca/floof/?ref=apilist.fun");
            string imageurl = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["image"];

            var embed = new DiscordEmbedBuilder()
            {
                Title = "UwU",
                ImageUrl = imageurl,
                Color = DiscordColor.Cyan
            };

            await ctx.CreateResponseAsync(embed);
        }
    }
}
