using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using annoyjorsh.Interfaces;
using Discord;
using Discord.WebSocket;

namespace annoyjorsh.Commands
{
    public class HunterDiceCommand : ICommand
    {
        public HunterDiceCommand() { }
        public async Task Execute(SocketMessage message)
        {
            string rollContent = message.Content.Replace("!hroll ", "");
            var match = Regex.Match(rollContent, @"^(\d{1,3})\s(\d{1,3})\s?(\d{1,3})?$");
            if (!match.Success)
            {
                return;
            }
            var difficulty = int.Parse(match.Groups[1].Value);
            var diceCount = int.Parse(match.Groups[2].Value);
            var modifier = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;
            if (diceCount > 50)
            {
                await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + " my hands aren't big enough to roll over 50 dice");
                return;
            }
            var rolls = new int[diceCount];
            var rnd = new Random();
            for (int diceRoll = 0; diceRoll < diceCount; diceRoll++)
            {
                rolls[diceRoll] = rnd.Next(1, 10);
            }
            string reply = " rolled " + string.Join(", ", rolls);
            int passes = rolls.Count(roll => roll >= difficulty);
            int hardFails = rolls.Count(roll => roll == 1);
            reply += " totaling ";
            if (modifier > 0)
            {
                passes += modifier;
                reply += passes + " passes (with modifier)";
            }
            else
            {
                reply += passes + " passes ";
            }
            if (hardFails > 0)
            {
                reply += " and " + hardFails + " failures";
            }
            else
            {
                reply += " and no failures";
            }

            await message.Channel.SendMessageAsync(MentionUtils.MentionUser(message.Author.Id) + reply);
        }

        public bool Match(SocketMessage message)
        {
            return message.Content.StartsWith("!hroll ");
        }

    }
}