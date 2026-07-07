using Domain.Filters.Interfaces;

namespace Infrastructure.Engine.FFmpeg.Models;

public class FilterNode
{
    public IFilter Filter { get; }
    public string InputLabel { get; }
    public string OutputLabel { get; }

    public FilterNode(IFilter filter, string inputLabel, string outputLabel)
    {
        Filter = filter;
        InputLabel = inputLabel;
        OutputLabel = outputLabel;
    }
}
