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

        public static Item testItem = new Item() { cost = 999999999, Name = "testItem", EmojiId = 921271332589674517 };
        public static Item papiez = new Item() { cost = 2137, Name = "John Paul II", EmojiId = 962735533791006770 };
        public static Item cum = new Item() { cost = 2137, Name = "Cum", EmojiId = 962735533791006770 };
    }
}