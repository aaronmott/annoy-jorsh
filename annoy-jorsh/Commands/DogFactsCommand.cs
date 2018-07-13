using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using annoyjorsh.Helpers;
using annoyjorsh.Interfaces;
using Discord.WebSocket;

namespace annoyjorsh.Commands
{
    public class DogFactsCommand : ICommand
    {
        private static IEnumerable<string> dogTriggers = new List<string>();
        private DiscordSocketClient bot;
        
        public DogFactsCommand(DiscordSocketClient _bot)
        {
            bot = _bot;
            dogTriggers = dogTriggers.Append("dog");
        }

        public async Task Execute(SocketMessage message)
        {
            await message.Channel.SendMessageAsync(await FactHelper.getDogFact());
        }

        public bool Match(SocketMessage message)
        {
            return dogTriggers.Any(s => message.Content.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1);
        }

    }
}