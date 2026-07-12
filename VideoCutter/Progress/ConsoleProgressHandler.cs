using Application.Engine.Services.Interfaces;

namespace VideoCutter.Progress;

internal class ConsoleProgressHandler : IProgressHandler
{
    private const int MaxVisibleBars = 10;
    private const int ProgressBarWidth = 25;

    private double[] _progress;    

    private int _totalSegments;
    private int _totalVisibleBars;

    private readonly Lock _lock;

    public ConsoleProgressHandler()
    {            
        _lock = new Lock();
    }

    public void Start(int totalSegments)
    {
        _totalSegments = totalSegments;
        _totalVisibleBars = Math.Min(MaxVisibleBars, totalSegments);

        _progress = new double[_totalSegments];

        Console.CursorVisible = false;
    }

    public void Handle(int segmentIndex, double percentage)
    {
        using (_lock.EnterScope())
        {
            _progress[segmentIndex] = percentage;

            Display();
        }
    }

    public void Finish()
    {
        Console.SetCursorPosition(0, 1);
    }

    private void Display()
    {
        DisplayGeneralInfo(0, 0);
        DisplayProgressBars(0, 3);

        Console.SetCursorPosition(0, 29);
    }    

    private void DisplayGeneralInfo(int left, int top)
    {
        int numberСompleted = 0;

        for (int index = 0; index < _progress.Length; index++)
        {
            if (_progress[index] >= 100)
                numberСompleted++;
        }

        Console.SetCursorPosition(left, top);

        DisplayProgressBar("Total progress", (double)numberСompleted / _totalSegments * 100);
    }

    private void DisplayProgressBars(int left, int top)
    {
        int visibleBarIndex = 0;

        Console.SetCursorPosition(left, top);
        for (int index = 0; index < _progress.Length; index++)
        {
            if (_progress[index] >= 100 || _progress[index] == 0)
                continue;

            visibleBarIndex++;

            if (visibleBarIndex > _totalVisibleBars)
                return;

            DisplayProgressBar($" [{index:000}] ", _progress[index]);
            Console.WriteLine();
        }

        for(int i = visibleBarIndex; i < _totalVisibleBars; i++)
            ClearLine(top + i);
    }

    private void DisplayProgressBar(string label, double percentage)
    {
        int filledPart = (int)Math.Round(ProgressBarWidth * percentage / 100.0);
        int emptyPart = ProgressBarWidth - filledPart;                       

        Console.Write(label);

        Console.Write(" [");
        Console.Write(new string('■', filledPart));
        Console.Write(new string('-', emptyPart));
        Console.Write("] ");

        Console.Write($"{percentage,5:F1} %");
    }

    private static void ClearLine(int top)
    {
        Console.WriteLine(new string(' ', Console.BufferWidth), top);
    }
}
