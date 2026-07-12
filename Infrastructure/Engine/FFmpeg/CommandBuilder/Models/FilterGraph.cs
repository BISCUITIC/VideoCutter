namespace Infrastructure.Engine.FFmpeg.CommandBuilder.Models;

public class FilterGraph
{
    public IReadOnlyCollection<FilterNode> Nodes { get; }

    public string? OutputVideoLabel { 
        get =>  Nodes.LastOrDefault()?.OutputLabel;
    }

    public bool IsEmpty { 
        get => Nodes.Count == 0; 
    }

    public FilterGraph(IReadOnlyCollection<FilterNode> nodes)
    {
        Nodes = nodes;                
    }
}
