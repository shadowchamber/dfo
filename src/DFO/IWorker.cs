using System.Threading;
using System.Threading.Tasks;

namespace DFO
{
    public interface IWorker
    {
        string[] Args { get; set; }

        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
