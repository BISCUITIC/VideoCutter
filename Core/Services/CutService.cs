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

    public CutService(TimeSpan videoDuration, 
                      TimeSpan chunkDuration, 
                      TimeSpan? offset = null, 
                      int? numberIteration = null) 
    {
        _videoDuration = videoDuration;
        _chunkDuration = chunkDuration;
        _offSet = offset ?? TimeSpan.Zero;

        _curreentIteration = 0;
        _numberIteration = 0;

        if (numberIteration < 0)
            throw new ArgumentOutOfRangeException($"{nameof(numberIteration)} can not be negative");

        if (numberIteration is null)
        {
            Init();
        }
        else
        {
            if (numberIteration.Value > MaxNumberIteration())
                throw new ArgumentOutOfRangeException($"{nameof(numberIteration)} can not be that big");

            _numberIteration = numberIteration.Value;
        }
    }

    private int MaxNumberIteration()
    {                   
        double videoSecondsDuration = _videoDuration.TotalSeconds;
        double chunkSecondsDuration = _chunkDuration.TotalSeconds;
        double offSetSecondsDuration = _offSet.TotalSeconds;

        return (int)Math.Ceiling((videoSecondsDuration - offSetSecondsDuration) / chunkSecondsDuration);
    }

    private void Init()
    {
        _numberIteration = MaxNumberIteration();
    }

    public void Process(FFMpegArgumentOptions options)
    {
        options.Seek(_offSet + _chunkDuration * _curreentIteration).WithDuration(_chunkDuration);      
    }
}
