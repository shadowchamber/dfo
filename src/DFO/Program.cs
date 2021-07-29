// <copyright file="Program.cs" company="Shadowchamber">
// Copyright (c) Shadowchamber. All rights reserved.
// </copyright>

namespace DFO
{
    using System.Threading;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Main application class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">startup arguments.</param>
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            var host = builder.Build();
            var service = host.Services.GetRequiredService<IWorker>();

            service.Args = args;

            CancellationToken cancellationToken = CancellationToken.None;
            var task = service.ExecuteAsync(cancellationToken);
            task.Wait();
        }

        /// <summary>
        /// Host builder creation.
        /// </summary>
        /// <param name="args">startup arguments.</param>
        /// <returns>host builder object.</returns>
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