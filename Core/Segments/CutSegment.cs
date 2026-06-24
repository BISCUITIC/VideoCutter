using Core.Segments.Interfaces;
using FFMpegCore;
using System.Text;

namespace Core.Segments;


internal class CutSegment : IPiplineSegment
{
    public TimeSpan? StartTime { get; }
    public TimeSpan? EndTime { get; }

    public CutSegment(TimeSpan start, TimeSpan end)
    {
        StartTime = start;
        EndTime = end;

        Console.WriteLine(this);
    }

    public void Apply(FFMpegArgumentOptions options, StringBuilder filterArgument)
    {
        options.Seek(StartTime)
               .WithDuration(EndTime - StartTime);               
    }

    public override string ToString()
    {
        return $"Cut segment : start {StartTime} ; end {EndTime}";
    }
}
