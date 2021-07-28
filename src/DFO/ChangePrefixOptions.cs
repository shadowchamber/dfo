using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFO
{
    [Verb("changeprefix", HelpText = "Change model prefix")]
    public class ChangePrefixOptions
    {
        [Option('f', "from-prefix", Required = true, HelpText = "From prefix")]
        public string FromPrefix { get; set; }

        [Option('t', "to-prefix", Required = true, HelpText = "To prefix")]
        public string ToPrefix { get; set; }

        [Option('p', "path", Required = true, HelpText = "Model folder path")]
        public string Path { get; set; }

        [Option('s', "source-prefix", Required = true, HelpText = "Source extension prefix")]
        public string SourcePrefix { get; set; }

        [Option('d', "dest-prefix", Required = true, HelpText = "Destination extension prefix")]
        public string DestinationPrefix { get; set; }
    }
}
