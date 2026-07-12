namespace VideoCutter.Progress.Interfaces;

internal interface IProgressBar
{
    void Display(int left, int top, string label, double percentage);
}
