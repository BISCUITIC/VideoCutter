using Core.Models;
using FFMpegCore;

namespace Core.Services.ServicesFactories;

public class VideoHandlerFactory
{
    private readonly SessionInfo _session;
    private readonly TimeSpan _videoDuration;
    private readonly TimeSpan _chunkDuration;

    public VideoHandlerFactory(SessionInfo session)
    {
        _videoDuration = FFProbe.Analyse(session.InputFilePath).Duration;
        _chunkDuration = TimeSpan.Parse("00:00:15");

        _session = session;
    }

    public VideoHandler Create()
    {
        CutService cutService = new CutService(_videoDuration, _chunkDuration);

        return new VideoHandler(cutService, _session);
    }
}
