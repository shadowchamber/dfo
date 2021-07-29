// <copyright file="Worker.cs" company="Shadowchamber">
// Copyright (c) Shadowchamber. All rights reserved.
// </copyright>

namespace DFO
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using CommandLine;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Main worker class.
    /// </summary>
    public class Worker : IWorker
    {
        private readonly ILogger<Worker> logger;
        private readonly IChangePrefixService changePrefixService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="changePrefixService">change prefix service.</param>
        public Worker(
            ILogger<Worker> logger,
            IChangePrefixService changePrefixService)
        {
            this.logger = logger;
            this.changePrefixService = changePrefixService;
        }

        /// <inheritdoc/>
        public string[] Args { get; set; }

        /// <inheritdoc/>
        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var parseResult = CommandLine.Parser.Default.ParseArguments<ChangePrefixOptions>(this.Args);
            await parseResult.WithParsedAsync(this.RunOptionsAsync).ConfigureAwait(false);
            await parseResult.WithNotParsedAsync(this.HandleParseErrorAsync).ConfigureAwait(false);
        }

        private async Task RunOptionsAsync(ChangePrefixOptions opts)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            await this.changePrefixService.ExecuteAsync(opts, cancellationToken).ConfigureAwait(false);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task HandleParseErrorAsync(IEnumerable<Error> errs)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            foreach (var err in errs)
            {
                this.logger.LogError(err.ToString());
            }
        }
    }
}