using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace rollItBot
{
    public class Bot
    {
        public DiscordClient client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public async Task RunAsnyc()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
                };
            client = new DiscordClient(config);


            var commandConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new String[] {configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                DmHelp = true,
                IgnoreExtraArguments = true
            };

            Commands = client.UseCommandsNext(commandConfig);
            Commands.RegisterCommands<RollCommand>();

            await client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
