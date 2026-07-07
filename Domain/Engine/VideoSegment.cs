namespace Domain.Engine;

public class VideoSegment
{
    public TimeSpan Start { get; }
    public TimeSpan End { get; }

    public VideoSegment(TimeSpan start, TimeSpan end)
    {
        Start = start;
        End = end;
    }
}
