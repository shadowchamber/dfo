using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DFO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            var host = builder.Build();
            var service = host.Services.GetRequiredService<IWorker>();

            service.Args = args;

            CancellationToken cancellationToken = new CancellationToken();
            var task = service.ExecuteAsync(cancellationToken);
            task.Wait();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IChangePrefixService, ChangePrefixService>();
                    services.AddSingleton<IWorker, Worker>();
                });
        }
    }
}

