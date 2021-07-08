using BotCore.Services;
using DiscordBotDatabase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bot_project
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PollContext>(options =>
            {
                options.UseNpgsql((@"your server address here"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

             services.AddScoped<IPollService, PollService>();
            var serviceProvider = services.BuildServiceProvider();

            var bot = new Bot(serviceProvider);
            services.AddSingleton(bot);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
