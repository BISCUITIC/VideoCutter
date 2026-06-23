using Core.Segments.Interfaces;
using FFMpegCore;

namespace Core.Segments;

public enum AspectMode
{
    Crop,
    Pad
}

internal class ResizeSegment : IPiplineSegment
{
    public int Width { get; }
    public int Height { get; }
    public AspectMode Mode { get; }


    public ResizeSegment(int width, int height, AspectMode mode)
    {
        Width = width;
        Height = height;
        Mode = mode;

        Console.WriteLine(this);
    }

    public void Apply(FFMpegArgumentOptions options)
    {
        Console.WriteLine("asd");
    }

    public override string ToString()
    {
        return $"Resize segment : width {Width}, height {Height}, mode {Mode}";
    }
}
