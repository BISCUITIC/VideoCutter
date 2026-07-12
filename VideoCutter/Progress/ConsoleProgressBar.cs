using VideoCutter.Progress.Interfaces;

namespace VideoCutter.Progress;

internal class ConsoleProgressBar : IProgressBar
{
    private const int Width = 25;

    public void Display(int left, int top, string label, double percentage)
    {
        Console.SetCursorPosition(left, top);

        Console.Write(label);

        DisplayBarBody(percentage);        

        Console.Write($"{percentage,5:F1} %");
    }

    private void DisplayBarBody(double percentage)
    {
        Console.Write(" [");
        Console.Write(new string('■', GetFilledPart(percentage)));
        Console.Write(new string('-', GetEmptyPart(percentage)));
        Console.Write("] ");
    }

    private int GetFilledPart(double percentage) => 
        (int) Math.Round(Width * percentage / 100.0);
    private int GetEmptyPart(double percentage) => 
        Width - GetFilledPart(percentage);
}
