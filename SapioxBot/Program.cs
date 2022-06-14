using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;
using DSharpPlus.SlashCommands;
using SapioxBot.Commands;
using SapioxBot.Currency;
using System.Linq;

namespace SapioxBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "Njg2Mjc3MDMwNDgzODUzNDA2.GXwx3s.ebepx8CV6Suk40Pg99AKX0vTtWyU2OeL_-FMeY",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            discord.GuildMemberAdded += async (s, e) =>
            {
                await e.Guild.GetChannel(666683529324789772).ModifyAsync(new(x => x.Name = $"» Członkowie: {e.Guild.MemberCount}"));
            };
            
            discord.MessageCreated += async (s, e) =>
            {
                /*if (e.Message.Content.ToLower() == "ping")
                {
                    await e.Message.RespondAsync("pong!");
                }*/
            };

            discord.ComponentInteractionCreated += async (s, e) => 
            {
                //settings choose code
                if (e.Id == "settings_menu")
                {
                    var embed = new DiscordEmbedBuilder()
                    {
                        Title = "🦊 SapioxBot Settings",
                        Description = $"Language: ``{Translate.CurrentLanguage}``",
                        Color = DiscordColor.Orange
                    };

                    var options = new List<DiscordSelectComponentOption>()
                    {
                        new DiscordSelectComponentOption("Curency", "currency"),
                        new DiscordSelectComponentOption("Other", "other", isDefault: true)
                    };

                    var dropdown = new DiscordSelectComponent("settings_menu", null, options);
                }
            };

            discord.Ready += async (s, e) =>
            {
                Items.Itemlist.Add(Items.testItem);
                Items.Itemlist.Add(Items.amogus);
                await discord.UpdateStatusAsync(new DiscordActivity() { Name = "👍", ActivityType = ActivityType.Streaming }, UserStatus.Online);
            };

            var slash = discord.UseSlashCommands();
            slash.RegisterCommands<OtherCommands>();
            slash.RegisterCommands<PSBIG>(464854486226305036);
            slash.RegisterCommands<CwanStars>(464854486226305036);
            slash.RegisterCommands<CurrencyCommands>();
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}