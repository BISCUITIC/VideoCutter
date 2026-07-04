namespace Application.Configuration.Interfaces;

public interface IConfigReader
{
    string Read (string path);
    Task<string> ReadAsync (string path);
}
