using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace rollItBot
{
    public class RollCommand : BaseCommandModule
    {
        [Command("roll")]
        public async Task Roll(CommandContext ctx, String rollValues)
        {
            rollValues = Regex.Replace(rollValues, @"\s+", "");
            MatchCollection matches = Regex.Matches(rollValues, "(\\d+)d(\\d+)(\\+\\d*|-\\d*|)");
            Random random = new Random();
            int instances = int.Parse(matches[0].Groups[1].Value);
            int roll = 1 + random.Next(int.Parse(matches[0].Groups[2].Value));
            int sum = 0;
            for (int i = 1; i <= instances; i++)
            {
                sum += (1 * roll);
                roll = 1 + random.Next(int.Parse(matches[0].Groups[2].Value));
            }
            String finalMatch = matches[0].Groups[3].Value;
            if (finalMatch.Length > 0)
            {
                if (finalMatch.StartsWith('+'))
                {
                    sum += int.Parse(finalMatch.Substring(1));
                } else
                {
                    sum -= int.Parse(finalMatch.Substring(1));
                }
            }
            await ctx.Channel.SendMessageAsync(ctx.Member.Mention + " rolled: " + sum.ToString()).ConfigureAwait(false);
        }
    }
}
