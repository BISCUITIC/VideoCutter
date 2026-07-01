namespace Infrastructure.Configuration.Services;

public class ConfigFileReader
{
    public string Read(string path)
    {
        return File.ReadAllText(path);
    }

    public Task<string> ReadAsync(string path)
    {
        return File.ReadAllTextAsync(path);
    }
}
