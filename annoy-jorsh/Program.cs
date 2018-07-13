using System;
using System.Linq;
using System.Threading.Tasks;
using annoyjorsh.Services;
using Microsoft.Extensions.DependencyInjection;
using annoyjorsh.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace annoyjorsh
{
    class Program
    {
        private static Random random = new Random();
        public static IConfigurationRoot Configuration;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(Configuration)
                .AddSingleton<IDiscordBot, DiscordBot>()                
                .BuildServiceProvider();
            serviceProvider.GetService<IDiscordBot>();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}
