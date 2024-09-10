using Discord.WebSocket;
using Discord;
using System;

namespace Discord_Bot
{
    public class Discord_Bot
    {

        public static Task Main(string[] args)
        {
            return new Bot(Convert.ToUInt64(args[0])).StartBot();
        }
    }
}