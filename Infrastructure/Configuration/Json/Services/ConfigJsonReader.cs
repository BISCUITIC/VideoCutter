using Application.Configuration.Interfaces;

namespace Infrastructure.Configuration.Json.Services;

public class ConfigJsonReader : IConfigReader
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
