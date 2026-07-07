namespace Infrastructure.Engine.FFmpeg.Models;

public class FilterGraph
{
    IReadOnlyCollection<FilterNode> Nodes { get; }
}
