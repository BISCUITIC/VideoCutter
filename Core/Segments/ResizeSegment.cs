using Core.Segments.Interfaces;
using FFMpegCore;
using System.Text;

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

    public string Apply()
    {
        return Mode switch
        {
            AspectMode.Crop => ApplyCrop(),
            AspectMode.Pad => ApplyPad(),
            _ => ""
        };
    }

    private string ApplyCrop()
    {        
        return $"scale={Width}:{Height}:force_original_aspect_ratio=increase," +
               $"crop={Width}:{Height};";
    }
    private string ApplyPad()
    {        
        return $"scale={Width}:{Height}:force_original_aspect_ratio=decrease," +
               $"pad={Width}:{Height}:(ow-iw)/2:(oh-ih)/2;";
    }

    public override string ToString()
    {
        return $"Resize segment : width {Width} ; height {Height} ; mode {Mode}";
    }
}
