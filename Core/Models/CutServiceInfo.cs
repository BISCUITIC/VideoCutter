namespace Core.Models;

public record class CutServiceInfo(
    int Iteration,
    TimeSpan Offset,
    TimeSpan Duration
);