using Application.Engine.Services.Interfaces;

namespace VideoCutter.Progress;

internal class ConsoleProgressHandler : IProgressHandler
{
    private readonly Dictionary<int, double> _progress;
    private readonly Lock _lock;

    private readonly int _cursorTop;
    private readonly int _progressBarWidth;

    public ConsoleProgressHandler()
    {
        _progress = new Dictionary<int, double>();
        _lock = new Lock();

        _cursorTop = Console.CursorTop;
        _progressBarWidth = 25;
    }

    private int CountBarPosition(int index) => index + _cursorTop;

    private void DrawProgressBar(int position, int index, double percentage)
    {

        int filledPart = (int)Math.Round(_progressBarWidth * percentage / 100.0);
        int emptyPart = _progressBarWidth - filledPart;

        Console.SetCursorPosition(0, position);
        Console.CursorVisible = false;

        Console.Write($"Сегмент {index:000}");

        Console.Write(" [");
        Console.Write(new string('■', filledPart));
        Console.Write(new string('-', emptyPart));
        Console.Write("] ");

        Console.Write($"{percentage,5:F1} %");
    }

    private void Display()
    {
        foreach (int index in _progress.Keys)
        {
            _progress.TryGetValue(index, out double progress);
            DrawProgressBar(CountBarPosition(index), index, progress);
        }
    }

    public void Handle(int segmentIndex, double percentage)
    {
        using (_lock.EnterScope())
        {
            _progress[segmentIndex] = percentage;

            Display();
        }
    }

}
