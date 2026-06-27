using Core.Segments.Interfaces;
using FFMpegCore;
using System.Text;

namespace Core.Segments;

internal class TextSegment : IPiplineSegment
{
    public string Text { get; }

    public string FontPath { get; }
    public int FontSize { get; }
    public string FontColor { get; }

    public int BorderWidth { get; }
    public string BorderColor { get; }

    public TextSegment(string text = "Laybal", 
                       string? fontPath = null, 
                       int? fontSize = null, 
                       string? fontColor = null, 
                       int? borderWidth = null, 
                       string? borderColor = null)
    {
        Text = text;

        FontPath = fontPath ?? "C\\:/Windows/Fonts/arial.ttf";
        FontSize = fontSize ?? 75;
        FontColor = fontColor ?? "White";

        BorderWidth = borderWidth ?? 10;
        BorderColor = borderColor ?? "Black";

        Console.WriteLine(this);
    }

    public void Apply(FFMpegArgumentOptions options, StringBuilder filterArgument)
    {        
        filterArgument.Append($"drawtext=text='{Text}':x=(w-text_w)/2:y=100:" +
                              $"fontfile='{FontPath}':" +
                              $"fontsize={FontSize}:" +
                              $"fontcolor={FontColor}:" +
                              $"borderw={BorderWidth}:" +
                              $"bordercolor={BorderColor},");
    }
}
