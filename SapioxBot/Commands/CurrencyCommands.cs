using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using SapioxBot.Currency;
using SapioxBot.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapioxBot.Commands
{
    public class CurrencyCommands : ApplicationCommandModule
    {
        [SlashCommand("bal", "Pokazuje ile masz na koncie")]
        public async Task Bal(InteractionContext ctx)
        {
            using (var Database = DatabaseManager.Database)
            {
                var Users = Database.GetCollection<User>("users");
                if (Users.FindOne(x => x.Id == ctx.User.Id) == null)
                {
                    var user = new User
                    {
                        Id = ctx.User.Id
                    };

                    Users.Insert(user);
                }
                else
                {
                    var embed = new DiscordEmbedBuilder()
                    {
                        Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"Portfel {ctx.User.Username}", IconUrl = ctx.User.AvatarUrl },
                        Description = $"**{Users.FindOne(x => x.Id == ctx.User.Id).Coins} $**",
                        Color = DiscordColor.Cyan
                    };
                    await ctx.CreateResponseAsync(embed);
                }
            }
        }

        [SlashCommand("inventory", "Pokazuje twoje inventory")]
        public async Task Inventory(InteractionContext ctx)
        {
            using (var Database = DatabaseManager.Database)
            {
                var Users = Database.GetCollection<User>("users");

                if (Users.FindOne(x => x.Id == ctx.User.Id) == null)
                {
                    var user = new User
                    {
                        Id = ctx.User.Id
                    };

                    Users.Insert(user);
                }
                else
                {
                    var User = Users.FindOne(x => x.Id == ctx.User.Id);

                    string description = "";
                    foreach (Item item in User.Items)
                    {
                        description += ("\n- " + DiscordEmoji.FromGuildEmote(ctx.Client, item.EmojiId) + " " + item.Name);
                    }

                    var embed = new DiscordEmbedBuilder()
                    {
                        Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = ctx.User.Username + "'s inventory", IconUrl = ctx.User.AvatarUrl },
                        Description = description,
                        Color = DiscordColor.Cyan
                    };
                    await ctx.CreateResponseAsync(embed);
                }
            }
        }

        /*[SlashCommand("give_item", "admin only command")]
        public async Task GiveItem(InteractionContext ctx)
        {
            try
            {
                using (var Database = DatabaseManager.Database)
                {
                    var Users = Database.GetCollection<User>("users");
                    var user = Users.FindOne(x => x.Id == ctx.User.Id);
                    user.Items.Add(Items.testItem);
                    Users.Update(user);

                    await ctx.CreateResponseAsync("test");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }*/

        [SlashCommand("shop", "item shop")]
        public async Task Shop(InteractionContext ctx)
        {
            string description = "";
            foreach (Item item in Items.Itemlist)
            {
                description += ("\n- " + DiscordEmoji.FromGuildEmote(ctx.Client, item.EmojiId) + " " + item.Name + " — ``" + item.cost + "$``");
            }

            var embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = "Sapiox Shop", IconUrl = ctx.Client.CurrentUser.AvatarUrl },
                Description = description,
                Color = DiscordColor.Blurple
            };
            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("buy", "kup item")]
        public async Task Buy(InteractionContext ctx, [Option("item_name", "Item name")] string item_name)
        {
            using (var Database = DatabaseManager.Database)
            {
                var Users = Database.GetCollection<User>("users");

                if (Users.FindOne(x => x.Id == ctx.User.Id) == null)
                {
                    var user = new User
                    {
                        Id = ctx.User.Id
                    };

                    Users.Insert(user);
                }
                var User = Users.FindOne(x => x.Id == ctx.User.Id);

                var item = Items.Itemlist.Find(x => x.Name.ToLower() == item_name.ToLower());

                if (item == null) await ctx.CreateResponseAsync("Niepoprawna nazwa itemu!");
                else
                {
                    if (User.Coins >= item.cost)
                    {
                        User.Items.Add(item);
                        Users.Update(User);

                        var embed = new DiscordEmbedBuilder()
                        {
                            Description = "Pomyślnie zakupiono nowy item!",
                            Color = DiscordColor.Green
                        };
                        await ctx.CreateResponseAsync(embed);
                    }
                    else
                    {
                        var embed = new DiscordEmbedBuilder()
                        {
                            Description = "Jesteś zbyt biedny na to!",
                            Color = DiscordColor.Red
                        };
                        await ctx.CreateResponseAsync(embed);
                    }
                }
            }
        }

        /*[SlashCommand("work", "just work")]
        public async Task Work(InteractionContext ctx)
        {

            await ctx.CreateResponseAsync("ok");
        }*/

        [SlashCommand("daily", "Odbierz swoje codzienne coiny")]
        public async Task Daily(InteractionContext ctx)
        {
            using (var Database = DatabaseManager.Database)
            {
                var Users = Database.GetCollection<User>("users");

                if (Users.FindOne(x => x.Id == ctx.User.Id) == null)
                {
                    var user = new User
                    {
                        Id = ctx.User.Id
                    };

                    Users.Insert(user);
                }

                var User = Users.FindOne(x => x.Id == ctx.User.Id);

                User.Coins += 10000;
                Users.Update(User);

                var embed = new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"{ctx.User.Username}", IconUrl = ctx.User.AvatarUrl },
                    Description = "**10000 $** zostało dodane do twojego portfela!",
                    Color = DiscordColor.Aquamarine
                };

                await ctx.CreateResponseAsync(embed);
            }
        }

        [SlashCommand("monthly", "Odbierz swoje miesięczne coiny")]
        async Task Monthly(InteractionContext ctx)
        {
            using (var Database = DatabaseManager.Database)
            {
                var Users = Database.GetCollection<User>("users");

                if (Users.FindOne(x => x.Id == ctx.User.Id) == null)
                {
                    var user = new User
                    {
                        Id = ctx.User.Id
                    };

                    Users.Insert(user);
                }

                var User = Users.FindOne(x => x.Id == ctx.User.Id);

                User.Coins += 100000;
                Users.Update(User);

                var embed = new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"{ctx.User.Username}", IconUrl = ctx.User.AvatarUrl },
                    Description = "**100000 $** zostało dodane do twojego portfela!",
                    Color = DiscordColor.Aquamarine
                };

                await ctx.CreateResponseAsync(embed);
            }
        }
    }
}
