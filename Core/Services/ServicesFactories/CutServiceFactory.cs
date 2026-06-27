using Core.Models;

namespace Core.Services.ServicesFactories;

public class CutServiceFactory
{
    private readonly Dictionary<string,string> _params;
    private readonly TimeSpan _videoDuration;

    public CutServiceFactory(CutServiceDefinition definition, TimeSpan videoDuration) 
    {
        _params = definition.CutServiceParams;
        _videoDuration = videoDuration;
    }

    public CutService Create()
    {      
        TimeSpan videoDuration = _videoDuration;
        TimeSpan chunkDuration = TimeSpan.Parse(_params["duration"]);

        TimeSpan? offset;
        if (_params.TryGetValue(
            "offset", 
            out string? outputOffset)
        )
        {
            offset = TimeSpan.Parse(outputOffset);
        }
        else
        {
            offset = null;
        }

        int? numberIterations;
        if (_params.TryGetValue(
            "chunksLimit", 
            out string? outputNumberIterations)
        )
        {
            numberIterations = int.Parse(outputNumberIterations);
        }
        else
        {
            numberIterations = null;
        }
 
        return new CutService(videoDuration, chunkDuration, offset, numberIterations);
    }
}
