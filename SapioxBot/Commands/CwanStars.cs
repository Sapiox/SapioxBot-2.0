using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot.Commands
{

    public class CwanStars : ApplicationCommandModule
    {
        [SlashCommand("sl", "Status naszego serwera scpsl")]
        public async Task Sl(InteractionContext ctx)
        {
            try
            {
                WebClient client = new WebClient();
                string json1 = client.DownloadString("https://api.scpslgame.com/serverinfo.php?id=21882&key=LC3tp9mf07ClT2EFjMqDa3uV&players=true&list=true&info=true&version=true&nicknames=true");
                var json = JObject.Parse(json1);
                var PlayersList1 = json["Servers"][0]["PlayersList"];
                string PlayersList = string.Join("\n", PlayersList1);

                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"SCP SL | niewiem #1",
                    Description = $"🎮 Gracze: {json["Servers"][0]["Players"]}\n```{PlayersList}```\n❗Wersja gry: {json["Servers"][0]["Version"]}",
                    Color = DiscordColor.Green
                };

                await ctx.CreateResponseAsync(embed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}