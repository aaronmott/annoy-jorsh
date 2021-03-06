﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using annoyjorsh.Helpers;
using annoyjorsh.Interfaces;
using Microsoft.Extensions.Configuration;
using annoyjorsh.Commands;

namespace annoyjorsh.Services
{
    public class DiscordBot : IDiscordBot
    {
        private IEnumerable<ICommand> commands = new List<ICommand>();
        private readonly DiscordSocketClient _client;

        public DiscordBot(IConfigurationRoot config)
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.LoginAsync(TokenType.Bot, config["DiscordToken"]);
            _client.StartAsync();
            _client.MessageReceived += MessageReceived;
            commands = commands
                .Append(new CatFactsCommand())
                .Append(new DogFactsCommand())
                //.Append(new OneOnOneCommand(_client))
                .Append(new WheelCommand())
                .Append(new DiceCommand())
                .Append(new HunterDiceCommand())
                .Append(new InsultCommand())
                .Append(new ShoutCommand());
        }
        public async Task MessageReceived(SocketMessage message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(
                message.Author.ToString() + " " +
                message.Timestamp.ToLocalTime().ToString("MM/dd/yyyy hh:mmtt") + ": "
            );
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
                message.Content
            );
            //TODO: insert selling all message/user data to china
            if (message.Author.IsBot || message.Content.IndexOf("!ignore", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return;
            }
            if (message.Content.StartsWith("!proxy"))
            {
                await _client.GetGuild(270933643025711104).GetTextChannel(464896563651543080).SendMessageAsync(message.Content.Replace("!proxy ", ""));
            }
            var commandTasks = commands.AsParallel().Where(c => c.Match(message)).Select(async c => await c.Execute(message));
            await Task.WhenAll(commandTasks);
        }
        public Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
