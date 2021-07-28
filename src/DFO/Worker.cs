using CommandLine;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DFO
{
    public class Worker : IWorker
    {
        private readonly ILogger<Worker> logger;
        private readonly IChangePrefixService changePrefixService;

        public string[] Args { get; set; }

        public Worker(ILogger<Worker> logger,
            IChangePrefixService changePrefixService)
        {
            this.logger = logger;
            this.changePrefixService = changePrefixService;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var parseResult = CommandLine.Parser.Default.ParseArguments<ChangePrefixOptions>(Args);
            await parseResult.WithParsedAsync(RunOptionsAsync).ConfigureAwait(false);
            await parseResult.WithNotParsedAsync(HandleParseErrorAsync).ConfigureAwait(false);
        }

        async Task RunOptionsAsync(ChangePrefixOptions opts)
        {
            CancellationToken cancellationToken = new CancellationToken();
            await changePrefixService.ExecuteAsync(opts, cancellationToken).ConfigureAwait(false);
        }

        async Task HandleParseErrorAsync(IEnumerable<Error> errs)
        {
            foreach (var err in errs)
            {
                this.logger.LogError(err.ToString());
            }
        }
    }
}

