using Core.Models;
using FFMpegCore;

namespace Core.Services.ServicesFactories;

public class VideoHandlerFactory
{
    private readonly SessionInfo _session;   
    private readonly CutServiceFactory _cutServiceFactory;

    public VideoHandlerFactory(SessionInfo session, CutServiceFactory cutServiceFactory)
    {
        _session = session;
        _cutServiceFactory = cutServiceFactory;
    }

    public VideoHandler Create()
    {
        CutService cutService = _cutServiceFactory.Create();

        return new VideoHandler(cutService, _session);
    }
}
