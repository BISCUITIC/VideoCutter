using Core.Segments.Interfaces;
using FFMpegCore;

namespace Core.Segments;

internal class BlurSegment : IPiplineSegment
{
    public int Width { get; }
    public int Height { get; }

    public BlurSegment(int width, int height) 
    {
        Width = width;
        Height = height;

        Console.WriteLine(this);
    }

    public void Apply(FFMpegArgumentOptions options, string filters)
    {
        options.WithCustomArgument(
           $"-filter_complex \"[0:v]split[bg][fg];" +
           $"[bg]scale={Width}:{Height}:force_original_aspect_ratio=increase," +
           $"crop={Width}:{Height},boxblur=20:1[blurred];" +
           $"[fg]scale=iw:ih[fgout];" +
           $"[blurred][fgout]overlay=0:0\"");
    }

    public override string ToString()
    {
        return $"Blur segment : width {Width} ; height {Height}";
    }
}
