// <copyright file="IChangePrefixService.cs" company="Shadowchamber">
// Copyright (c) Shadowchamber. All rights reserved.
// </copyright>

namespace DFO
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Change prefix service interface.
    /// </summary>
    public interface IChangePrefixService
    {
        /// <summary>
        /// Executes command with options.
        /// </summary>
        /// <param name="options">command options.</param>
        /// <param name="stoppingToken">cancellation token.</param>
        /// <returns>async task.</returns>
        Task ExecuteAsync(ChangePrefixOptions options, CancellationToken stoppingToken);
    }
}