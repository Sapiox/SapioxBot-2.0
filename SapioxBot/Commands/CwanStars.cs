﻿using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot.Commands
{
    public class CwanStars : ApplicationCommandModule
    {
        [SlashCommand("villager", "villager")]
        public async Task villager(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync("https://media.discordapp.net/attachments/986244209596043274/986244248930250782/Snapchat-2102585422.jpg");
        }
    }
}