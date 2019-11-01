using System;
using System.Linq;
using System.Threading.Tasks;
using annoyjorsh.Interfaces;
using Discord;
using Discord.WebSocket;

namespace annoyjorsh.Commands
{
    public class OneOnOneCommand : ICommand
    {
        private DiscordSocketClient bot;
        public OneOnOneCommand(DiscordSocketClient _bot)
        {
            bot = _bot;
        }

        public async Task Execute(SocketMessage message)
        {
            await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + " 1v1 me dust 2 mid bro");
        }

        public bool Match(SocketMessage message)
        {
            return message.MentionedUsers.Select(x => x.Id).Contains(bot.CurrentUser.Id);
        }

    }
}