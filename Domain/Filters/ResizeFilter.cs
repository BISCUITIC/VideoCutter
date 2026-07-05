using Domain.Filters.Attributes;
using Engine.Filters.Interfaces;

namespace Engine.Filters;

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
