using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DFO
{
    public class ChangePrefixService : IChangePrefixService
    {
        private readonly ILogger<ChangePrefixService> logger;

        public ChangePrefixService(ILogger<ChangePrefixService> logger)
        {
            this.logger = logger;
        }

        public async Task ExecuteAsync(ChangePrefixOptions options, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Changing prefix From: {options.FromPrefix} To: {options.ToPrefix}");
        }
    }
}
