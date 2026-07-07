namespace Domain.Engine;

public class Command
{
    public string Argument { get; }

    public Command(string argument)
    {
        Argument = argument;
    }
}
