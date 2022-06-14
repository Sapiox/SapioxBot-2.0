﻿using DSharpPlus;
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
                Token = "Njg2MjA1NTg1NjM2NDU4NDk5.GM4SFo.Mm67DaDU2bLTwbNnHlR9colL_bogYJJVmtwPFY",
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
                //cwanstars recrutaiment
                //q in customId means question
                if (e.Id == "cw_rekru_apply_dc")
                {
                    var response = new DiscordInteractionResponseBuilder();

                    response
                      .WithTitle("Rekrutacja CwanStars")
                      .WithCustomId("cw_rekru_answer_dc")
                      .AddComponents(new TextInputComponent(label: "Ile masz lat?", customId: "cw_rekru_q1_dc", placeholder: "Wpisz swój wiek", max_length: 3))
                      .AddComponents(new TextInputComponent("Czy pełniłeś/aś kiedyś podobną funkcję?","cw_rekru_q2"))
                      .AddComponents(new TextInputComponent("Jeżeli tak to gdzie i jaką?", "cw_rekru_q3", required: false))
                      .AddComponents(new TextInputComponent("Opisz siebie!", "cw_rekru_q4", style: TextInputStyle.Paragraph));


                    await e.Interaction.CreateResponseAsync(InteractionResponseType.Modal, response);
                }

                if (e.Id == "cw_rekru_apply_sl")
                {
                    var response = new DiscordInteractionResponseBuilder();

                    response
                      .WithTitle("Rekrutacja CwanStars")
                      .WithCustomId("cw_rekru_answer_sl")
                      .AddComponents(new TextInputComponent(label: "Ile masz lat?", customId: "cw_rekru_q1_sl", placeholder: "Wpisz swój wiek", max_length: 3))
                      .AddComponents(new TextInputComponent("Czy pełniłeś/aś kiedyś podobną funkcję?", "cw_rekru_q2", max_length: 3))
                      .AddComponents(new TextInputComponent("Ile godzin masz w scpsl?", "cw_rekru_q3"))
                      .AddComponents(new TextInputComponent("Co zmieniłbyś na serwerze?", "cw_rekru_q4"))
                      .AddComponents(new TextInputComponent("Opisz siebie!", "cw_rekru_q5", style: TextInputStyle.Paragraph));

                    await e.Interaction.CreateResponseAsync(InteractionResponseType.Modal, response);
                }


                //settings choose code
                if (e.Id == "settings_menu")
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

            discord.ModalSubmitted += async (s, e) =>
            {
                if(e.Values.TryGetValue("cw_rekru_q1_sl", out string age))
                {
                    e.Values.TryGetValue("cw_rekru_q2", out string q2);
                    e.Values.TryGetValue("cw_rekru_q3", out string q3);
                    e.Values.TryGetValue("cw_rekru_q4", out string q4);
                    e.Values.TryGetValue("cw_rekru_q5", out string q5);

                    var embed = new DiscordEmbedBuilder()
                    {
                        Title = "Podanie na Moderatora",
                        Description =
                        $"Podanie napisane przez: {e.Interaction.User.Mention}"+
                        "\n\n**Typ: SL**\n\n" +
                        $"Ile masz lat?: {age}\n" +
                        $"Czy pełniłeś/aś kiedyś podobną funkcję?: {q2}\n" +
                        $"Ile godzin masz w scpsl?: {q3}\n" +
                        $"Co zmieniłbyś na serwerze?: {q4}\n" +
                        $"Opisz siebie!: {q5}"
                    };
                    await e.Interaction.Guild.GetChannel(975844919623163934).SendMessageAsync(embed);
                }
                if (e.Values.TryGetValue("cw_rekru_q1_dc", out string agee))
                {
                    e.Values.TryGetValue("cw_rekru_q2", out string q2);
                    e.Values.TryGetValue("cw_rekru_q3", out string q3);
                    e.Values.TryGetValue("cw_rekru_q4", out string q4);

                    var embed = new DiscordEmbedBuilder()
                    {
                        Title = "Podanie na Moderatora",
                        Description =
                        $"Podanie napisane przez: {e.Interaction.User.Mention}" +
                        "\n\n**Typ: Discord**\n\n" +
                        $"Ile masz lat?: {agee}\n" +
                        $"Czy pełniłeś/aś kiedyś podobną funkcję?: {q2}\n" +
                        $"Jeżeli tak to gdzie i jaką?: {q3}\n" +
                        $"Opisz siebie!: {q4}"
                    };
                    await e.Interaction.Guild.GetChannel(975844919623163934).SendMessageAsync(embed);
                }

                await e.Interaction.CreateResponseAsync(InteractionResponseType.AutoCompleteResult);
            };

            discord.Ready += async (s, e) =>
            {
                Items.Itemlist.Add(Items.testItem);
                Items.Itemlist.Add(Items.amogus);
                await discord.UpdateStatusAsync(new DiscordActivity() { Name = "tak", ActivityType = ActivityType.Streaming }, UserStatus.Online);
            };

            var slash = discord.UseSlashCommands();
            slash.RegisterCommands<OtherCommands>(464854486226305036);
            slash.RegisterCommands<CurrencyCommands>(464854486226305036);
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}