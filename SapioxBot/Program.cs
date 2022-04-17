using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using SapioxBot.Commands;
using SapioxBot.Currency;

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
                Token = "Njg2Mjc3MDMwNDgzODUzNDA2.XmU3Sg.j7_BOyGOhY15Yjd7_c9FKVuzhvY",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });
            
            discord.GuildMemberAdded += async (s, e) =>
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Description = $"Welcome to our server, {e.Member.Mention}!",
                    Color = DiscordColor.Cyan
                };
                
                await e.Guild.GetChannel(1).SendMessageAsync(embed);
            };
            
            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower() == "ping")
                {
                    await e.Message.RespondAsync("pong!");
                }

                if (e.Message.Author.Id == 357501136053338114)
                {
                    await e.Message.RespondAsync("Hello " + e.Author.Username);
                }
            };

            discord.ComponentInteractionCreated += async (s, e) => 
            {
                //settings choose code
                if(e.Id == "settings_menu")
                {
                    var embed = new DiscordEmbedBuilder()
                    {
                        Title = "🦊 SapioxBot Settings",
                        Color = DiscordColor.Orange
                    };

                    var options = new List<DiscordSelectComponentOption>()
                    {
                        new DiscordSelectComponentOption("SCPSL server inegration", "scpsl"),
                        new DiscordSelectComponentOption("Curency", "currency"),
                        new DiscordSelectComponentOption("Other", "other")
                    };

                    var dropdown = new DiscordSelectComponent("settings_menu", null, options);

                    if (e.Values.Contains("scpsl"))
                    {
                        string slservers = "";
                        using(var Database = DatabaseManager.Database)
                        {
                            var User = Database.GetCollection<Database.User>("users").FindOne(x => x.Id == e.User.Id);

                            foreach(Database.scpsl scpslserver in User.SCPSL_Servers)
                            {
                                slservers += $"- IP:{scpslserver.ip} Port: {scpslserver.Port}\n";
                            }
                        }

                        embed.WithFooter(" ", "https://static.wikia.nocookie.net/scp-secret-laboratory-official/images/a/af/SCP_SL_Logo.png");
                        string serversString = String.IsNullOrWhiteSpace(slservers) ? "- ``none``\n" : slservers;
                        embed.Description =
                            "**SCP:SL Integration**\n\n"+
                            "***✅ Available servers:***\n" +
                            serversString
                            ;

                        var button = new DiscordButtonComponent(ButtonStyle.Success, "add_server_sl", "Add Server", emoji: new DiscordComponentEmoji(935253091417202688));

                        var message = new DiscordInteractionResponseBuilder().AddEmbed(embed).AddComponents(button);

                        await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, message);
                    }
                }
            };

            discord.Ready += async (s, e) =>
            {
                Items.Itemlist.Add(Items.testItem);
                Items.Itemlist.Add(Items.papiez);
                Items.Itemlist.Add(Items.cum);
                await discord.UpdateStatusAsync(new DiscordActivity() { Name = "SapioxBot 2.0 soon...", ActivityType = ActivityType.Streaming }, UserStatus.Online);
            };

            var slash = discord.UseSlashCommands();
            slash.RegisterCommands<OtherCommands>(891716256414175312);
            slash.RegisterCommands<CurrencyCommands>(891716256414175312);
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}