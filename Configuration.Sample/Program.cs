using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Configuration.Sample
{
    class Program
    {
        private static readonly string SettingsFile = "Settings.json";

        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var settings = host.Services.GetService<IOptions<Settings>>();

            TestApplication TA = new(settings);
            Console.WriteLine(TA.DumpSettings());
            Console.ReadLine();// Test ReloadOnChange: true
            Console.WriteLine(TA.DumpSettings());

            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
                .ConfigureAppConfiguration((hosting, config) =>
                {
                    config.AddJsonFile(SettingsFile,
                        optional: false,
                        reloadOnChange: true);
                })
                .ConfigureServices((host, services) =>
                services.Configure<Settings>(Settings =>
                {
                    host.Configuration.GetSection(nameof(Settings)).Bind(Settings);
                }));

    }
}
