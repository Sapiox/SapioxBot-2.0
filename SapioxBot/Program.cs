﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;
using DSharpPlus.SlashCommands;
using SapioxBot.Commands;
using SapioxBot.Currency;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Token = "Njg2Mjc3MDMwNDgzODUzNDA2.G7CbVS.1Jc6NRxNdTZIcxJt_MkOaKt-8CUh-3bhA-iefs",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            discord.GuildMemberAdded += async (s, e) =>
            {
                if(e.Guild.Id == 464854486226305036)
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
            slash.RegisterCommands<ApplicationCommandModule>();
            //PSBIG
            slash.RegisterCommands<PSBIG>(867857272339693598);
            slash.RegisterCommands<CurrencyCommands>(867857272339693598);
            slash.RegisterCommands<OtherCommands>(867857272339693598);
            //CwanStars
            slash.RegisterCommands<CwanStars>(464854486226305036);
            slash.RegisterCommands<CurrencyCommands>(464854486226305036);
            slash.RegisterCommands<OtherCommands>(464854486226305036);
            //niewiem
            slash.RegisterCommands<CwanStars>(1031166390608089151);
            slash.RegisterCommands<CurrencyCommands>(1031166390608089151);
            slash.RegisterCommands<OtherCommands>(1031166390608089151);

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}