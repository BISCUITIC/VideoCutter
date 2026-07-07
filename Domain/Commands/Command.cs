namespace Domain.Commands;

public class Command
{
    public string Executer {  get; }
    public IReadOnlyCollection<Argument> Arguments { get; }

    public Command(string executer, IReadOnlyCollection<Argument> arguments)
    {
        Executer = executer;
        Arguments = arguments;
    }
}
