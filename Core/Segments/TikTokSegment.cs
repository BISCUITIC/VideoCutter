using Core.Segments.Interfaces;

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

    public string Apply()
    {
        return $"split[background][foreground];" +
               $"[background]" +
               $"scale={Width}:{Height}:force_original_aspect_ratio=increase," +
               $"crop={Width}:{Height},boxblur={Radius}:{Power}[blurred];" +
               $"[foreground]" +
               $"scale={Width}:{Height}:force_original_aspect_ratio=decrease[scaled];" +
               $"[blurred][scaled]overlay=(W-w)/2:(H-h)/2";
    }

    public override string ToString()
    {
        return $"Blur segment : width {Width} ; height {Height}";
    }
}
