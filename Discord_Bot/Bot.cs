using Discord;
using Discord.WebSocket;

namespace Discord_Bot
{
    public class Bot
    {
        // The Channel that is being used
        private IMessageChannel channel;
        //Discord client
        private DiscordSocketClient _client;

        private ulong channelID;
        private readonly string token;

        public Bot(ulong channelID)
        {
            if(Environment.GetEnvironmentVariable("DISCORD_TOKEN") is null || Environment.GetEnvironmentVariable("DISCORD_TOKEN") == "")
            {
                Console.WriteLine("Token is Empty");
                return;
            }
            if(channelID <= 0)
            {
                Console.WriteLine("ChannelID is Empty");
                return;
            }
            this.channelID = channelID;
            token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
        }

        public async Task StartBot()
        {
            // Initialize
            _client = new DiscordSocketClient();
            _client.Log += Log;

            // Login 
            await _client.LoginAsync(TokenType.Bot, token);

            // Channel setting
            channel = (IMessageChannel)await _client.GetChannelAsync(channelID); 

            // Connection Start
            await _client.StartAsync();
        }

        /// <summary>
        /// Stops the Discord bot
        /// </summary>
        /// <returns></returns>
        public async Task StopBot()
        {
            await _client.LogoutAsync();
            await _client.StopAsync();
        }

        /// <summary>
        /// Logs
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


        /// <summary>
        /// Notifies the channel with the newes comic with their data
        /// </summary>
        /// <param name="scanlator"></param>
        /// <param name="name"></param>
        /// <param name="chapter"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public async Task Announce(string scanlator, string name, string chapter, string link)
        {
            await channel!.SendMessageAsync($"```Scanlatorteam: {scanlator}\r\nName: {name}\r\nChapter name: {chapter}\r\nLink:```\r\n {link}");
        }

        /// <summary>
        /// Read the last msg from a channel
        /// Only needed for testing purposes
        /// </summary>
        /// <returns></returns>
        public async Task<IMessage> Read()
        {
            var a = await channel!.GetMessagesAsync(1).FlattenAsync();
            return a.First();
        }

        /// <summary>
        /// Delete all msg from a channel
        /// Only needed for testing purposes
        /// </summary>
        /// <returns></returns>
        public async Task Delete()
        {
            var msgs = await channel!.GetMessagesAsync(1).FlattenAsync();
            foreach(var msg in msgs)
            {
                await msg.DeleteAsync();
            }
        }

    }
}
