using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot.Commands
{
    public class PSBIG : ApplicationCommandModule
    {
        [SlashCommand("villager", "villager")]
        public async Task villager(InteractionContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Title = $"villager",
                ImageUrl = "https://media.discordapp.net/attachments/986244209596043274/986244248930250782/Snapchat-2102585422.jpg",
                Color = DiscordColor.Brown
            };

            await ctx.CreateResponseAsync(embed);
            //await ctx.CreateResponseAsync("https://media.discordapp.net/attachments/986244209596043274/986244248930250782/Snapchat-2102585422.jpg");
        }
    }
}
