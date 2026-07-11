using Application.Engine.Services.Interfaces;
using Domain.Commands;

namespace Infrastructure.Engine.FFmpeg.CommandExecuter;

public class CommandExecuter : ICommandExecutor
{
    public Task ExecuteAsync(Command command, 
                             CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
