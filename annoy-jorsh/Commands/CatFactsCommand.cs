using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using annoyjorsh.Helpers;
using annoyjorsh.Interfaces;
using Discord.WebSocket;

namespace annoyjorsh.Commands
{
    public class CatFactsCommand : ICommand
    {
        private static IEnumerable<string> catTriggers = new List<string>();

        public CatFactsCommand()
        {
            catTriggers = catTriggers.Append("cat");
        }

        public async Task Execute(SocketMessage message)
        {
            await message.Channel.SendMessageAsync(await FactHelper.getCatFact());
        }

        public bool Match(SocketMessage message)
        {
            return catTriggers.Any(s => message.Content.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1);
        }

    }
}