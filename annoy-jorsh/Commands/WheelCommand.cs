using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using annoyjorsh.Helpers;
using annoyjorsh.Interfaces;
using Discord.WebSocket;

namespace annoyjorsh.Commands
{
    public class WheelCommand : ICommand
    {
        public WheelCommand()
        {
        }

        public async Task Execute(SocketMessage message)
        {
            await message.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/418872941191626752/418872985768951808/wheel.png");
        }

        public bool Match(SocketMessage message)
        {
            return (message.Content.Equals("!wheel",StringComparison.InvariantCultureIgnoreCase));
        }

    }
}