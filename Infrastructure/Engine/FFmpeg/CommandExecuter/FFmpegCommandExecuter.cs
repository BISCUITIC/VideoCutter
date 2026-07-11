using Application.Engine.Services.Interfaces;
using Domain.Commands;
using FFMpegCore;
using System.Diagnostics;

namespace Infrastructure.Engine.FFmpeg.CommandExecuter;

public class FFmpegCommandExecuter : ICommandExecutor
{
    public async Task ExecuteAsync(Command command,
                                   CancellationToken cancellationToken = default)
    {
        //PrintCommand(command);

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = GlobalFFOptions.GetFFMpegBinaryPath(),
            UseShellExecute = false,
            CreateNoWindow = true
        };

        foreach (Argument argument in command.Arguments)
        {
            if (argument.Option is not null)
                startInfo.ArgumentList.Add(argument.Option);

            startInfo.ArgumentList.Add(argument.Value);
        }

        using var process = Process.Start(startInfo)!;

        await process.WaitForExitAsync(cancellationToken);
    }   

    private void PrintCommand(Command command)
    {
        foreach (Argument argument in command.Arguments)
        {
            Console.WriteLine($"{argument.Option} - {argument.Value}");
        }
    }
}
