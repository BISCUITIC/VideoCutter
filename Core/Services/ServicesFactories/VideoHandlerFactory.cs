using Core.Models;

namespace Core.Services.ServicesFactories;

public class VideoHandlerFactory
{
    private readonly Pipeline _pipeline;
    private readonly SessionInfo _session;
    private readonly CutServiceFactory _cutServiceFactory;

    public VideoHandlerFactory(Pipeline pipline, SessionInfo session, CutServiceFactory cutServiceFactory)
    {
        _pipeline = pipline;
        _session = session;
        _cutServiceFactory = cutServiceFactory;
    }

    public VideoHandler Create()
    {
        CutService cutService = _cutServiceFactory.Create();

        return new VideoHandler(_pipeline, cutService, _session);
    }
}
