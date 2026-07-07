namespace Domain.Commands;

public class Argument
{
    public string Option { get; }
    public string Value { get; }

    public Argument(string option, string value)
    {
        Option = option;
        Value = value;
    }
}
