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

    public void Apply(FFMpegArgumentOptions options, StringBuilder filterArgument)
    {
        switch (Mode)
        {
            case AspectMode.Crop:
                ApplyCrop(options, filterArgument);
                break;
            case AspectMode.Pad:
                ApplyPad(options, filterArgument);
                break;
            default:
                break;
        }
    }

    private void ApplyCrop(FFMpegArgumentOptions options, StringBuilder filterArgument)
    {
        //options.WithCustomArgument($"-vf \"scale={Width}:{Height}:force_original_aspect_ratio=increase,crop={Width}:{Height}\"");
        filterArgument.Append($"[0:v]scale={Width}:{Height}:force_original_aspect_ratio=increase," +
                              $"crop={Width}:{Height};");
    }
    private void ApplyPad(FFMpegArgumentOptions options, StringBuilder filterArgument)
    {
        //options.WithCustomArgument($"-vf \"scale={Width}:{Height}:force_original_aspect_ratio=decrease,pad={Width}:{Height}:(ow-iw)/2:(oh-ih)/2\"");
        filterArgument.Append($"[0:v]scale={Width}:{Height}:force_original_aspect_ratio=decrease," +
                              $"pad={Width}:{Height}:(ow-iw)/2:(oh-ih)/2;");
    }

    public override string ToString()
    {
        return $"Resize segment : width {Width} ; height {Height} ; mode {Mode}";
    }
}
