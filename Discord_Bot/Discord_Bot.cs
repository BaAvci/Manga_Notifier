using Discord.WebSocket;
using Discord;
using System;

namespace Manga_Notifier // Note: actual namespace depends on the project name.
{
    public class Discord_Bot
    {
        // Token for the discord bot
        private readonly string token = "MTE4MjY3OTczNzgwMjQ0NDg5MQ.Gpw98F.I9jHwv9QdIvSrRk-ge3xdpAa14LNagCJ_qOkUg";
        // The Channel that is being used
        private IMessageChannel channel;
        //Discord client
        private DiscordSocketClient _client;

        public static Task Main(string[] args) => new Discord_Bot().StartAsync(Convert.ToUInt64(args[0]));

        /// <summary>
        /// Starts the discord bot and sets the channel ID
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public async Task StartAsync(ulong channelID)
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            
            await _client.LoginAsync(TokenType.Bot, token);

            channel = (IMessageChannel)await _client.GetChannelAsync(channelID);

            await _client.StartAsync();

            //await Task.Delay(-1); Runs this task permanently
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
        /// Stops the Discord bot
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            await _client.StopAsync();
        }

        /// <summary>
        /// Notifies the channel with the newes comic with their data
        /// </summary>
        /// <param name="scanlator"></param>
        /// <param name="name"></param>
        /// <param name="chapter"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public async Task Announce(string scanlator,string name,string chapter, string link)
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
            foreach (var msg in msgs)
            {
                await msg.DeleteAsync();
            }
        }
    }
}