namespace VideoCutter.Progress.Interfaces;

internal interface IProcessingTimer : IDisposable
{
    void Display(int left, int top, string label);
}
