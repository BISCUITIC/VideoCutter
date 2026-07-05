using Engine.Filters.Interfaces;

namespace Domain.Definitions;

public class Pipeline
{
    public IReadOnlyCollection<IFilter> Filters { get; }

    public Pipeline(IReadOnlyCollection<IFilter> filters)
    {
        Filters = filters;
    }
}
