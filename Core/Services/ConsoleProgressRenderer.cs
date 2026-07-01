namespace Core.Services;

internal class ConsoleProgressRenderer
{
    private readonly int _cursorStartRow;
    private readonly int _chunkCount;

    public ConsoleProgressRenderer(int chunkCount)
    {
        _cursorStartRow = Console.CursorTop;
        Console.CursorVisible = false;

        _chunkCount = chunkCount;

        Init();
    }

    private void Init()
    {
        RenderBar("OVERALL",0 );
        for (int i = 0; i < _chunkCount; i++)            
            RenderBar($"CHUNK {i}", 0);
    }

    public void Update(int chunkIndex, double chunkPercent, double overallPercent)
    {
        UpdateOverall(overallPercent);
        UpdateChunk(chunkIndex, chunkPercent);
    }

    private void UpdateChunk(int index, double percent)
    {
        Console.SetCursorPosition(0, _cursorStartRow + index + 1);
        RenderBar($"CHUNK {index}", percent);
    }

    private void UpdateOverall(double percent)
    {
        Console.SetCursorPosition(0, _cursorStartRow);
        RenderBar("OVERALL", percent);
    }

    private void RenderBar(string label, double percent)
    {
        int width = 20;
        int filled = (int)(percent / 100 * width);

        string bar = "[" + new string('█', filled) + new string('-', width - filled) + "]";

        Console.WriteLine($"{label}: {bar} {percent,5:0.0}%");
    }
}
