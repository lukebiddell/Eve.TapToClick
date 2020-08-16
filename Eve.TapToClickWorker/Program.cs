using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Eve.TapToClickWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(l =>
                {
                    l.AddConsole(c =>
                    {
                        // The default log format uses 2 lines for every message, and
                        // imo, it is really hard to read. The systemd format is a lot
                        // nicer.
                        c.Format = Microsoft.Extensions.Logging.Console.ConsoleLoggerFormat.Systemd;
                    });
                    l.AddDebug();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                }).UseWindowsService();
    }
}
