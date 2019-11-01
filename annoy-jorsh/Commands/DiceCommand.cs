using System;
using System.Linq;
using System.Threading.Tasks;
using annoyjorsh.Interfaces;
using Discord;
using Discord.WebSocket;

namespace annoyjorsh.Commands
{
    public class DiceCommand : ICommand
    {
        public DiceCommand() { }
        public async Task Execute(SocketMessage message)
        {
            string rollContent = message.Content.Replace("!roll ", "");
            bool diceSet = rollContent.IndexOf("d", StringComparison.OrdinalIgnoreCase) >= 0;
            int number;
            bool isNumeric = Int32.TryParse(rollContent, out number);
            if (!diceSet && !isNumeric)
            {
                return;
            }
            Random rnd = new Random();
            if (!diceSet && isNumeric)
            {
                await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + " rolled " + rnd.Next(1, Math.Abs(number)));
                return;
            }
            else if (diceSet)
            {
                var diceParts = rollContent.Split("d");
                if (diceParts.Length == 2)
                {
                    int diceCount;
                    bool parsedCount = Int32.TryParse(diceParts[0], out diceCount);
                    int diceType;
                    bool parsedType = Int32.TryParse(diceParts[1], out diceType);
                    if (parsedCount && parsedType && diceCount > 0 && diceType > 0)
                    {
                        if (diceCount > 50)
                        {
                            await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + " my hands aren't big enough to roll over 50 dice");
                            return;
                        }
                        if (diceType > 120)
                        {
                            await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + " where do you get your dice? nothing over a d120 please");
                            return;
                        }
                        var rolls = new int[diceCount];
                        for (int diceRoll = 0; diceRoll < diceCount; diceRoll++)
                        {
                            rolls[diceRoll] = rnd.Next(1, Math.Abs(diceType));
                        }
                        await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + " rolled " + string.Join(", ", rolls));
                        return;
                    }
                }
            }
        }

        public bool Match(SocketMessage message)
        {
            return message.Content.StartsWith("!roll ");
        }

    }
}