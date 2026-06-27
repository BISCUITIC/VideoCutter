using FFMpegCore;

namespace Core.Services;

public class CutService
{
    private TimeSpan _chunkDuration;
    private TimeSpan _videoDuration;
    private TimeSpan _offSet;

    private int _curreentIteration;
    private int _numberIteration;

    public int CurrentIteration { get => _curreentIteration; }
    public bool CanMoveNext() { return _curreentIteration < _numberIteration; }
    public void MoveNext() { 
        if (!CanMoveNext())
            throw new ArgumentOutOfRangeException($"Iteration {_curreentIteration} is out of range");
        _curreentIteration++;
    }
    public int NumberIteration { get => _numberIteration; }

    public CutService(TimeSpan videoDuration, TimeSpan outputDuration, TimeSpan? offset = null)
    {
        _videoDuration = videoDuration;
        _chunkDuration = outputDuration;
        _offSet = offset ?? TimeSpan.Zero;

        _curreentIteration = 0;
        _numberIteration = 0;

        Init();
    }

    private void Init()
    {
        double videoSecondsDuration = _videoDuration.TotalSeconds;
        double chunkSecondsDuration = _chunkDuration.TotalSeconds;
        double offSetSecondsDuration = _offSet.TotalSeconds;

        _numberIteration = (int)Math.Ceiling((videoSecondsDuration - offSetSecondsDuration) / chunkSecondsDuration);
    }

    public void Process(FFMpegArgumentOptions options)
    {
        options.Seek(_offSet + _chunkDuration * _curreentIteration).WithDuration(_chunkDuration);      
    }
}
