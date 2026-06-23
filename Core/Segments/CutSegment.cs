using Core.Segments.Interfaces;

namespace Core.Segments;


public class CutSegment : IPiplineSegment
{
    public TimeSpan? StartTime { get; }
    public TimeSpan? EndTime { get; }

    public CutSegment(TimeSpan start, TimeSpan end)
    {
        StartTime = start;
        EndTime = end;

        Console.WriteLine(this);
    }

    public void Apply()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"Cut segment : start {StartTime} ; end {EndTime}";
    }
}
