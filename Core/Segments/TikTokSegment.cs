using Core.Segments.Interfaces;
using FFMpegCore;
using System.Text;

namespace Core.Segments;

internal class TikTokSegment : IPiplineSegment
{
    public int Width { get; }
    public int Height { get; }

    public int Radius { get; }
    public int Power { get; }

    public TikTokSegment(int width, int height, int radius, int power) 
    {
        Width = width;
        Height = height;

        Radius = radius;
        Power = power;

        Console.WriteLine(this);
    }

    public void Apply(FFMpegArgumentOptions options, StringBuilder filterArgument)
    {        
        filterArgument.Append("[0:v]split[background][foreground];")
                      .Append("[background]")
                      .Append($"scale={Width}:{Height}:force_original_aspect_ratio=increase,")
                      .Append($"crop={Width}:{Height},boxblur={Radius}:{Power}[blurred];")
                      .Append("[foreground]")
                      .Append($"scale={Width}:{Height}:force_original_aspect_ratio=decrease[scaled];")
                      .Append("[blurred][scaled]overlay=(W-w)/2:(H-h)/2,");        
    }

    public override string ToString()
    {
        return $"Blur segment : width {Width} ; height {Height}";
    }
}
