using Domain.Filters.Attributes;
using Domain.Filters.Interfaces;

namespace Domain.Filters;

[FilterType("Resize")]
public class ResizeFilter : IFilter
{
    public int Width { get; }
    public int Height { get; }

    public ResizeFilter(int width, int height)
    {
        Width = width;
        Height = height;
    }
}
