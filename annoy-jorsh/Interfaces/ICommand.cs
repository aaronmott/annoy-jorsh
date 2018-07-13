using System.Threading.Tasks;
using Discord.WebSocket;

namespace annoyjorsh
{
    public interface ICommand
    {
        bool Match(SocketMessage message);
        Task Execute(SocketMessage message);
    }
}