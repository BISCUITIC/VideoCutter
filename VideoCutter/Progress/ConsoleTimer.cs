using System.Reflection.Emit;
using System.Threading;
using System.Timers;
using VideoCutter.Progress.Interfaces;
using Timer = System.Timers.Timer;

namespace VideoCutter.Progress;

internal class ConsoleTimer : IProcessingTimer
{
    private readonly Timer _timer;
    private DateTime _startTime;
    private TimeSpan _elapsedTime;

    public ConsoleTimer()
    {

        _startTime = DateTime.Now;
        _elapsedTime = TimeSpan.Zero;

        _timer = new Timer(100);
        _timer.Elapsed += OnTimeEvent;
        _timer.AutoReset = true; 
        _timer.Enabled = true;
    }

    public void Display(int left, int top, string label)
    {
        Console.SetCursorPosition(left, top);

        Console.Write(label);
        Console.Write($@" | {_elapsedTime:hh\:mm\:ss\.f} ");
    }

    public void Dispose()
    {
        _timer.Elapsed -= OnTimeEvent;
    }

    private void OnTimeEvent(Object? source, ElapsedEventArgs e)
    {
        _elapsedTime = e.SignalTime - _startTime;        
    }    
}
