using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot.Database
{
    public class scpsl
    {
        public int ServerId { get; set; }
        public string ip { get; set; }
        public int Port { get; set; }
        public ulong status_channel_id { get; set; }
        public ulong Gamelogs_channel_id { get; set; }
        public ulong RAlogs_channel_id { get; set; }
    }
}
