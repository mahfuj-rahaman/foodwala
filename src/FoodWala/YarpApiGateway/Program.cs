using Microsoft.AspNetCore.RateLimiting;

namespace YarpApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            builder.Services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
                {
                    options.Window = TimeSpan.FromSeconds(10);
                    options.PermitLimit = 5;
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseRateLimiter();

            app.MapReverseProxy();

            app.Run();


            //var builder = WebApplication.CreateBuilder(args);
            //var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            //app.Run();
        }
    }
}
