using DSharpPlus;
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

        [SlashCommand("hosting", "Sapiox hosting info")]
        public async Task Hosting(InteractionContext ctx)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("https://randomfox.ca/floof/?ref=apilist.fun");
            string imageurl = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["image"];

            var embed = new DiscordEmbedBuilder()
            {
                Title = "Our Hosting",
                ImageUrl = imageurl,
                Color = DiscordColor.Blurple
            };

            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("rekrutacja", "Zgłoś się na moderatora!")]
        public async Task test(InteractionContext ctx)
        {
            var button_discord = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "cw_rekru_apply_dc", "Aplikuj na DC Moderator", emoji: new DiscordComponentEmoji(972801912510500914));
            var button_sl = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "cw_rekru_apply_sl", "Aplikuj na SL Moderator", emoji: new DiscordComponentEmoji(414540014417084426));

            var embed = new DiscordEmbedBuilder()
            {
                Title = "***__REKRUTACJA NA MODERATORA__***",
                Description = "Zgłoś się na Moderatora!\nSprawdzamy zgłoszenia codziennie, a jeśli ktoś nam się spodoba to do niego napiszemy.",
                Color = DiscordColor.Green
            };

            var message = new DiscordInteractionResponseBuilder().AddEmbed(embed).AddComponents(button_discord, button_sl);

            await ctx.CreateResponseAsync(message);
        }

        [SlashCommand("test", "test")]
        public async Task testt(InteractionContext ctx)
        {
            var response = new DiscordInteractionResponseBuilder();

            response
              .WithTitle("Super cool modal!")
              .WithCustomId("my-modal")
  .AddComponents(new TextInputComponent(label: "Ile masz lat?", customId: "cw_rekru_q1", placeholder: "Wpisz swój wiek", max_length: 3))
  .AddComponents(new TextInputComponent("Czy pełniłeś/aś kiedyś podobną funkcję?", "why-fav", "Because it tastes good"));
             //.AddComponents(new TextInputComponent(label: "Czy pełniłeś/aś kiedyś podobną funkcję? Jeżeli tak to gdzie i jaką?", customId: "cw_rekru_q2", max_length: 300))
              //.AddComponents(new TextInputComponent(label: "Opisz siebie!", customId: "cw_rekru_q3", max_length: 300));

            await ctx.CreateResponseAsync(InteractionResponseType.Modal, response);
        }
    }
}
