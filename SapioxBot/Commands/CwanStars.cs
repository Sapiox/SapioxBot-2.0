using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        [SlashCommand("sl", "sl status")]
        public async Task Sl(InteractionContext ctx)
        {
            try
            {
                Console.WriteLine("test0");
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://api.scpslgame.com/serverinfo.php?id=20819&key=ExNm2hgECsbuf83%2F0wMyIgtD&players=true&list=true&info=true&version=true&nicknames=true"))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var list = JObject.Parse(apiResponse)["Attributes"].Select(el => new { Key = (string)el["Key"] }).ToList();
                        var Keys = list.Select(p => p.Key).ToList();

                        Console.WriteLine(Keys);
                    }
                }
                /*string playerList = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["PlayerList"];
                string name = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["Info"];
                string version = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)["Version"];

                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"SCP SL | {Convert.FromBase64String(name)}",
                    Description = $"🎮 Gracze: {playerCount}\n```{playerList}```\n❗Wersja gry: {version}",
                    Color = DiscordColor.Green
                };
                Console.WriteLine("test2");*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            await ctx.CreateResponseAsync("tf");
            Console.WriteLine("test3");
        }
    }
}