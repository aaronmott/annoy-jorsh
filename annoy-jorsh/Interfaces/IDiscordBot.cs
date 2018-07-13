using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace annoyjorsh.Interfaces
{
    public interface IDiscordBot
    {
        Task MessageReceived(SocketMessage message);
        Task Log(LogMessage message);
    }
}
