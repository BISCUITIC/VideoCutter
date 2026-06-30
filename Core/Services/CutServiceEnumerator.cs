using Core.Models;
using System.Collections;

namespace Core.Services;

public class CutServiceEnumerator : IEnumerator<CutServiceInfo>
{    
    private readonly TimeSpan _duration;
    private readonly TimeSpan _offset;

    private readonly int _numberIteration;
    private int _currentIteration;

    public CutServiceInfo Current => new CutServiceInfo(_currentIteration, _offset, _duration);
    object IEnumerator.Current => Current;

    public CutServiceEnumerator(int numberIteration,TimeSpan duration, TimeSpan offset)
    {
        _numberIteration = numberIteration;

        _duration = duration;
        _offset = offset;

        Reset();
    }

    public bool MoveNext()
    {      
        if (_currentIteration >= _numberIteration)
            return false;

        _currentIteration++;
        return true;
    }

    public void Reset()
    {
        _currentIteration = -1;
    }

    public void Dispose()
    {
       
    }
}
