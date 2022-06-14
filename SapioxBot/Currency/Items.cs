using SapioxBot.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot.Currency
{
    public class Items
    {
        public static  List<Item> Itemlist = new List<Item>();

        public static Item testItem = new Item() { cost = 999999999, Name = "testItem", EmojiId = 689807417269420053 };
        public static Item amogus = new Item() { cost = 2137, Name = "Amogus", EmojiId = 755876606693736488 };
    }
}