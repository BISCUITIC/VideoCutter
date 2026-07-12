using Application.Engine.Services.Interfaces;
using System.Data;
using VideoCutter.Progress.Interfaces;

namespace VideoCutter.Progress;

internal class ConsoleProgressHandler : IProgressHandler
{

    private const int MaxVisibleBars = 10;    

    private readonly IProgressBar _progressBar;

    private double[] _progress;
    private bool[] _completed;

    private int _totalSegmentsCount;
    private int _completedSegmentsCount;

    private int _visibleBarsCount;

    private readonly Lock _lock;

    private double GetTotalProgressPercentage =>
        (double)_completedSegmentsCount / _totalSegmentsCount * 100;

    public ConsoleProgressHandler(IProgressBar progressBar)
    {            
        _progressBar = progressBar;
        _lock = new Lock();
    }

    public void Start(int totalSegments)
    {
        _totalSegmentsCount = totalSegments;
        _completedSegmentsCount = 0;

        _visibleBarsCount = Math.Min(MaxVisibleBars, totalSegments);

        _progress = new double[_totalSegmentsCount];
        _completed = new bool[_totalSegmentsCount];

        Console.CursorVisible = false;
    }    

    public void Handle(int index, double percentage)
    {
        using (_lock.EnterScope())
        {
            if(IsSegmentBecomeCompleted(index, percentage))
            {
                _completedSegmentsCount++;
                _completed[index] = true;
            }

            UpdatePercentage(index, percentage);

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
        DisplayProgressBars(0, 2);
    }    

    private void DisplayGeneralInfo(int left, int top)
    {
        _progressBar.Display(
            left, top, "Total progress", GetTotalProgressPercentage
        );
    }

    private void DisplayProgressBars(int left, int top)
    {
        int visibleBarIndex = 0;       

        for (int index = 0; index < _progress.Length; index++)
        {
            if (!IsSegmentProcessing(index))
                continue;

            visibleBarIndex++;

            if (visibleBarIndex > _visibleBarsCount)
                return;

            _progressBar.Display(
                left, top + visibleBarIndex, $" [{index:000}] ", _progress[index]
            );
        }

        ClearСompleteBars(left, top, visibleBarIndex);
    }

    private void ClearСompleteBars(int left, int top, int visibleBarIndex)
    {
        for (int i = visibleBarIndex + 1; i < _visibleBarsCount; i++)
        {
            Console.SetCursorPosition(left, top + i);
            Console.Write(new string(' ', Console.BufferWidth - left));
        }
    }

    private bool IsSegmentProcessing(int index)
    {
        return _progress[index] != 0 && _completed[index] == false;
    }

    private bool IsSegmentBecomeCompleted(int index, double newPercentage)
    {
        double previousPersentage = _progress[index];

        if (previousPersentage < newPercentage && newPercentage == 100)
            return true;

        return false;
    }

    private void UpdatePercentage(int index, double newPercentage)
    {
        _progress[index] = newPercentage;
    }
}
