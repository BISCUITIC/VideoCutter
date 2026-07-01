namespace Core.Services;

internal class ProgressTracker
{
    private readonly double[] _progress;

    public event Action<int, double, double> OnChange;

    public ProgressTracker(int chunkCount)
    {
        _progress = new double[chunkCount];
    }

    public void Report(int chunkIndex, double progress)
    {
        _progress[chunkIndex] = progress;

        double totalProgress = _progress.Average();
        
        OnChange.Invoke(chunkIndex, progress, totalProgress);
    }
}
