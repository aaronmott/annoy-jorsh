using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using annoyjorsh.Helpers;
using annoyjorsh.Interfaces;
using Discord.WebSocket;

namespace annoyjorsh.Commands
{
    public class ShoutCommand : ICommand
    {
        private static IEnumerable<string> shoutTriggers = new List<string>();

        public ShoutCommand()
        {
            shoutTriggers = shoutTriggers.Append("shout").Append("yell");
        }

        public async Task Execute(SocketMessage message)
        {
            var cleanedMessage = Regex.Replace(message.Content.Replace("shout", ""), @"\s+", " ");
            var shout = ShoutHelper.searchShouts(cleanedMessage.Split(" ").Where(x => x.Length > 2));
            await message.Channel.SendMessageAsync("_" + shout.Key + "!_ - " + shout.Value);
        }

        public bool Match(SocketMessage message)
        {
            return shoutTriggers.Any(s => message.Content.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1);
        }

    }
}