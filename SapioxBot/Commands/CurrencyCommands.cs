﻿using DSharpPlus.Entities;
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
        [SlashCommand("bal", "shows your coin balance")]
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
                        Title = $"{ctx.User.Username}'s Balance",
                        Description = Users.FindOne(x => x.Id == ctx.User.Id).Coins + " $",
                        Color = DiscordColor.Cyan
                    };
                    await ctx.CreateResponseAsync(embed);
                }
            }
        }

        [SlashCommand("inventory", "shows your invetory")]
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

        [SlashCommand("give_item", "admin only command")]
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
        }

        [SlashCommand("shop", "items shop")]
        public async Task Shop(InteractionContext ctx)
        {
            string description = "";
            foreach (Item item in Items.Itemlist)
            {
                description += ("\n- " + DiscordEmoji.FromGuildEmote(ctx.Client, item.EmojiId) + " " + item.Name + " — ``" + item.cost + "$``");
            }

            var embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = "Shop", IconUrl = ctx.Client.CurrentUser.AvatarUrl },
                Description = description,
                Color = DiscordColor.Blurple
            };
            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("buy", "buy an item")]
        public async Task Work(InteractionContext ctx, string item_name)
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

                    var item = Items.Itemlist.Find(x => x.Name == item_name);

                    if (item == null) await ctx.CreateResponseAsync("Invalid item name!");
                    else
                    {
                        if(item.cost >= User.Coins)
                        {
                            User.Items.Add(item);
                            Users.Update(User);

                            var embed = new DiscordEmbedBuilder()
                            {
                                Description = "Sucess, you now have a new item!",
                                Color = DiscordColor.Green
                            };
                            await ctx.CreateResponseAsync(embed);
                        }
                        else
                        {
                            var embed = new DiscordEmbedBuilder()
                            {
                                Description = "Bruh you too poor, go to work!",
                                Color = DiscordColor.Red
                            };
                            await ctx.CreateResponseAsync(embed);
                        }
                    }
                }
            }
        }
    }
}
