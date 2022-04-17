using SapioxBot.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot.Database
{
    public class User
    {
        public ulong Id { get; set; }
        public int Coins { get; set; } = 0;
        public List<Item> Items { get; set; } = new List<Item>();
        public List<scpsl> SCPSL_Servers { get; set; } = new List<scpsl>();
    }
}
