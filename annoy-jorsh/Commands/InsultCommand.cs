using System.Linq;
using System.Threading.Tasks;
using annoyjorsh.Helpers;
using annoyjorsh.Interfaces;
using Discord;
using Discord.WebSocket;

namespace annoyjorsh.Commands
{
    public class InsultCommand : ICommand
    {
        private DiscordSocketClient bot;
        
        public InsultCommand(DiscordSocketClient _bot)
        {
            bot = _bot;
        }
        public async Task Execute(SocketMessage message)
        {
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

        public bool Match(SocketMessage message)
        {
            return message.Content.StartsWith("!insult");
        }
    }
}