using Serilog;

namespace Vista_iCT_BegrippenLijst
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .UseSerilog()
                .Build();
            await host.RunAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    config
                    .AddJsonFile("appsettings.json", false)
                    .AddJsonFile($"appsettings.{environmentName}.json", true)
                    .AddJsonFile("serilog.json", false)
                    .AddEnvironmentVariables();

                }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }

    }
}