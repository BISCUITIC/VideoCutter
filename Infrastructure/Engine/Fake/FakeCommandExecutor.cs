using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Processing;

namespace Infrastructure.Engine.Fake;

public class FakeCommandExecutor : ICommandExecutor
{
    public async Task ExecuteAsync(
        Command command,
        VideoSegment segment,
        Action<double>? progress = null,
        CancellationToken cancellationToken = default)
    {
        for (double percentage = 0;
             percentage <= 100;
             percentage += 1)
        {
            cancellationToken.ThrowIfCancellationRequested();

            progress?.Invoke(percentage);

            await Task.Delay(Random.Shared.Next(30, 100),cancellationToken);
        }
    }
}
