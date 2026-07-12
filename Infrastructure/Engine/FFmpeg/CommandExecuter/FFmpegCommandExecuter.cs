using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Processing;
using FFMpegCore;
using System.Text;

namespace Infrastructure.Engine.FFmpeg.CommandExecuter;

public class FFmpegCommandExecuter : ICommandExecutor
{    
    public async Task ExecuteAsync(Command command,
                                   VideoSegment segment,
                                   Action<double>? progress = null,
                                   CancellationToken cancellationToken = default)
    {
        string inputFilePath = GetInputFilePath(command);
        string outputFilePath = GetOutputFilePath(command);
        string customArguments = BuildCustomArguments(command);

        await FFMpegArguments.FromFileInput(inputFilePath)
                             .OutputToFile(
                                 outputFilePath,
                                 true,
                                 option => option.WithCustomArgument(customArguments)
                             )
                             .NotifyOnProgress(
                                percentage => progress?.Invoke(percentage), 
                                segment.Duration
                             )
                             .CancellableThrough(cancellationToken)
                             .ProcessAsynchronously();
    }

    private string GetInputFilePath(Command command)
    {
        Argument argument = command.Arguments
                                   .SingleOrDefault(argument => argument.Option == "-i") ??
                                   throw new InvalidOperationException("Команда FFmpeg не содержит входной файл");

        return argument.Value;
    }

    private string GetOutputFilePath(Command command)
    {
        Argument argument = command.Arguments
                                   .SingleOrDefault(argument => argument.Option is null) ??
                                   throw new InvalidOperationException("Команда FFmpeg не содержит выходной файл");

        return argument.Value;
    }

    private string BuildCustomArguments(Command command)
    {
        StringBuilder builder = new StringBuilder();

        foreach (Argument argument in command.Arguments)
        {
            if (argument.Option != "-i" && argument.Option is not null)
                builder.Append($"{argument.Option} {argument.Value} ");
        }

        return builder.ToString();
    }
}
