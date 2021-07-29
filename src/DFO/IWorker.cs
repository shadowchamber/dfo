// <copyright file="IWorker.cs" company="Shadowchamber">
// Copyright (c) Shadowchamber. All rights reserved.
// </copyright>

namespace DFO
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Main worker interface.
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// Gets or sets startup arguments.
        /// </summary>
        string[] Args { get; set; }

        /// <summary>
        /// Executes the command by arguments.
        /// </summary>
        /// <param name="stoppingToken">cancellation token.</param>
        /// <returns>async task.</returns>
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}