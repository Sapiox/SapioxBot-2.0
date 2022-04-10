using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot.Database
{
    public class Item
    {
        public int cost { get; set; }
        public string Name { get; set; }
        public ulong EmojiId { get; set; }
    }
}
