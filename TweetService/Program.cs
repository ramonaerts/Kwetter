using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Shared.Messaging;

namespace TweetService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var timer = new System.Timers.Timer { Interval = 30000 };
            timer.Elapsed += TimerElapsed;
            timer.Start();
            CreateHostBuilder(args).Build().Run();
        }

        static void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            QueuedTasks.ExecuteAction();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
