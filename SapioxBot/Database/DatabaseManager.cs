using LiteDB;
using SapioxBot.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot
{
    public class DatabaseManager
    {
        public static LiteDatabase Database => new LiteDatabase(@"database.db");

        public static ILiteCollection<User> Users => Database.GetCollection<User>("users");
    }
}
