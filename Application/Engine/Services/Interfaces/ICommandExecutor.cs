using Domain.Engine;

namespace Application.Engine.Services.Interfaces;

public interface ICommandExecutor
{
    Task ExecuteAsync(Command command, CancellationToken cancellationToken = default)
}
