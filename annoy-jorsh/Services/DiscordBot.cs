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

namespace annoyjorsh.Services
{
    public class DiscordBot : IDiscordBot
    {
        private static List<string> catTriggers = new List<string>() { "cat" };
        private static List<string> dogTriggers = new List<string>() { "dog" };
        private static List<string> unitTriggers = new List<string>() { "unit" };
        private readonly DiscordSocketClient _client;

        public DiscordBot(IConfigurationRoot config)
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;

            _client.LoginAsync(TokenType.Bot, config["DiscordToken"]);
            _client.StartAsync();
            _client.MessageReceived += MessageReceived;
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
            if (message.MentionedUsers.Select(x => x.Id).Contains(_client.CurrentUser.Id)){
                await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + " 1v1 me dust 2 mid bro");
            }
            if (message.Author.IsBot || message.Content.IndexOf("!ignore", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return;
            }
            if (catTriggers.Any(s => message.Content.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1))
            {
                await message.Channel.SendMessageAsync(await FactHelper.getCatFact());
            }
            if (dogTriggers.Any(s => message.Content.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1))
            {
                await message.Channel.SendMessageAsync(await FactHelper.getDogFact());
            }
            if(message.Content.IndexOf("subscribe",StringComparison.OrdinalIgnoreCase) != -1)
            {
                //annoyjorsh.Program.
            }
            if(message.Content.StartsWith("!insult")){
                var sendMessage = "";
                var mentioned = message.MentionedUsers.Any();
                var plural = message.MentionedUsers.Count > 1;
                // if(message.Author.ToString() == "username#8939"){
                //     sendMessage += MentionUtils.MentionUser(_client.GetUser("username","8939").Id) + " is a ";
                //     mentioned = false;
                //     plural = false;
                // }
                if(mentioned){
                    message.MentionedUsers.ToList().ForEach(mu => {
                        if (plural && message.MentionedUsers.Count -1 == message.MentionedUsers.ToList().IndexOf(mu)){
                            sendMessage += " and ";
                        }
                        sendMessage += MentionUtils.MentionUser(mu.Id);
                        if (plural && message.MentionedUsers.Count -2 > message.MentionedUsers.ToList().IndexOf(mu)){
                            sendMessage += ", ";
                        }

                    });
                }
                if (plural){
                    sendMessage += " are ";
                }
                else if (mentioned){
                    sendMessage += " is a ";
                }
                sendMessage += InsultGenerator.getQuickInsult(4,plural);
                await message.Channel.SendMessageAsync(sendMessage);
            }
            //posterity sakes....
            //if (unitTriggers.Any(s => message.Content.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1))
            //{
            //    await message.Channel.SendMessageAsync(MentionUtils.MentionUser(_client.GetUser("J o R s H", "7726").Id) + " is a bad unit");
            //}
        }
        public Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
