using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using annoyjorsh.Helpers;
using annoyjorsh.Interfaces;
using Quartz;
using Microsoft.Extensions.Configuration;
using annoyjorsh.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace annoyjorsh.Services
{
    public static class DiscordService
    {
        private static IEnumerable<ICommand> commands = new List<ICommand>();

        public static IServiceCollection AddDiscord(this IServiceCollection services)
        {
            services.AddTransient(CreateDiscordProvider);
            return services;
        }
        private static DiscordSocketClient CreateDiscordProvider(IServiceProvider serviceProvider)
        {
            var client = new DiscordSocketClient();
            client.Log += Log;
            client.MessageReceived += MessageReceived;
            commands = commands
                .Append(new CatFactsCommand())
                .Append(new DogFactsCommand())
                .Append(new OneOnOneCommand(client))
                .Append(new InsultCommand());
            return client;
        }
        public static void StartDiscord(this DiscordSocketClient client, string token)
        {            
            client.LoginAsync(TokenType.Bot, token);
            client.StartAsync();
        }
        private static async Task MessageReceived(SocketMessage message)
        {
            Console.WriteLine(message.Channel.Id + ": " + message.Channel.Name);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(
                message.Author.ToString() + " " +
                message.Timestamp.ToLocalTime().ToString("MM/dd/yyyy hh:mmtt") + ": "
            );
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(                                
                message.Content
            );
            if (message.Author.IsBot || message.Content.IndexOf("!ignore", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return;
            }
            var commandTasks = commands.AsParallel().Where(c => c.Match(message)).Select(async c => await c.Execute(message));
            await Task.WhenAll(commandTasks);
        }


        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
