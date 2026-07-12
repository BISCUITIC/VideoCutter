namespace Domain.Processing;

public class VideoSegment
{
    public TimeSpan Start { get; }
    public TimeSpan End { get; }
    public TimeSpan Duration { get => End - Start; }

    public VideoSegment(TimeSpan start, TimeSpan end)
    {
        Start = start;
        End = end;
    }
}
