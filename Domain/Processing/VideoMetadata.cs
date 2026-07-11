namespace Domain.Processing;

public class VideoMetadata
{
    public TimeSpan Duration { get; }

    public VideoMetadata(TimeSpan duration)
    {
        Duration = duration;
    }
}
