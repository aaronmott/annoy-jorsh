using annoyjorsh.Interfaces;
using annoyjorsh.Models;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Events;
using Tweetinvi.Models;
using Tweetinvi.Streaming;
using Tweetinvi.Streaming.Parameters;

namespace annoyjorsh.Services
{
    public static class TwitterService
    {
        private static DiscordSocketClient _discord;

        public static IServiceCollection AddTwitter(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient(createTwitterProvider);
            return services;
        }

        public static IFilteredStream createTwitterProvider(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            Auth.SetUserCredentials(config["TwitterConsumerKey"], config["TwitterConsumerSecret"], config["TwitterAccessToken"], config["TwitterAccessSecret"]);
            return Tweetinvi.Stream.CreateFilteredStream();
        }

        public static void startTwitterStream(this IFilteredStream stream, DiscordSocketClient discordService)
        {
            _discord = discordService;
            var authenticatedUser = User.GetUserFromScreenName("stairmaster401");
            stream.AddFollow(authenticatedUser);            
            stream.MatchingTweetReceived += (sender, args) => TweetRecieved(sender, args);
            stream.StartStreamMatchingAnyCondition();
        }

        private static async void TweetRecieved(object sender, MatchedTweetReceivedEventArgs args)
        {
            // Do what you want with the Tweet.
            Console.WriteLine(args.Tweet);
        }

    }
}
