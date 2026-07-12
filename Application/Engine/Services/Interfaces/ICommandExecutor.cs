using Domain.Commands;
using Domain.Processing;

namespace Application.Engine.Services.Interfaces;

public interface ICommandExecutor
{    
    Task ExecuteAsync(Command command, 
                      VideoSegment segment,
                      Action<double>? progress = null,
                      CancellationToken cancellationToken = default);
}
