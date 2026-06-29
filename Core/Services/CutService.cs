using Core.Models;
using FFMpegCore;
using System.Collections;

namespace Core.Services;

public class CutService: IEnumerable<CutServiceInfo>
{
    private TimeSpan _chunkDuration;
    private TimeSpan _videoDuration;
    private TimeSpan _offSet;
    
    private int _numberIteration;
    
    public int NumberIteration { get => _numberIteration; }

    public CutService(TimeSpan videoDuration,
                      TimeSpan chunkDuration,
                      TimeSpan? offset = null,
                      int? numberIteration = null)
    {
        _videoDuration = videoDuration;
        _chunkDuration = chunkDuration;
        _offSet = offset ?? TimeSpan.Zero;

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

    public void Process(FFMpegArgumentOptions options, CutServiceInfo info)
    {
        options.Seek(info.Offset +  info.Duration * info.Iteration).WithDuration(_chunkDuration);
    }

    public IEnumerator<CutServiceInfo> GetEnumerator()
    {
        return new CutServiceEnumerator(_numberIteration, _chunkDuration, _offSet);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
