// <copyright file="ChangePrefixOptions.cs" company="Shadowchamber">
// Copyright (c) Shadowchamber. All rights reserved.
// </copyright>

namespace DFO
{
    using CommandLine;

    /// <summary>
    /// Change prefix command options.
    /// </summary>
    [Verb("changeprefix", HelpText = "Change model prefix")]
    public class ChangePrefixOptions
    {
        /// <summary>
        /// Gets or sets from prefix value.
        /// </summary>
        [Option('f', "from-prefix", Required = true, HelpText = "From prefix")]
        public string FromPrefix { get; set; }

        /// <summary>
        /// Gets or sets to prefix value.
        /// </summary>
        [Option('t', "to-prefix", Required = true, HelpText = "To prefix")]
        public string ToPrefix { get; set; }

        /// <summary>
        /// Gets or sets model folder path.
        /// </summary>
        [Option('p', "path", Required = true, HelpText = "Model folder path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets source extension prefix.
        /// </summary>
        [Option('s', "source-prefix", Required = true, HelpText = "Source extension prefix")]
        public string SourcePrefix { get; set; }

        /// <summary>
        /// Gets or sets destination extension prefix.
        /// </summary>
        [Option('d', "dest-prefix", Required = true, HelpText = "Destination extension prefix")]
        public string DestinationPrefix { get; set; }
    }
}
