using System;
using System.Linq;
using System.Text.RegularExpressions;
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
                var match = Regex.Match(rollContent, @"^(\d{1,3})\s?d\s?(\d{1,3})\s?\+?\s?(\d{1,2})?$");
                if (!match.Success)
                {
                    return;
                }
                var diceCount = int.Parse(match.Groups[1].Value);
                var diceType = int.Parse(match.Groups[2].Value);
                var modifier = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;
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
                string reply = " rolled " + string.Join(", ", rolls);
                int total = rolls.Sum();
                reply += " totaling ";
                if (modifier > 0)
                {
                    total += modifier;
                    reply += total + " (with modifier)";
                }
                else
                {
                    reply += total;
                }
                await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + reply);
            }
        }

        public bool Match(SocketMessage message)
        {
            return message.Content.StartsWith("!roll ");
        }

    }
}