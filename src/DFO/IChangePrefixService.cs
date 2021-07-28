using System.Threading;
using System.Threading.Tasks;

namespace DFO
{
    public interface IChangePrefixService
    {
        Task ExecuteAsync(ChangePrefixOptions options, CancellationToken stoppingToken);
    }
}