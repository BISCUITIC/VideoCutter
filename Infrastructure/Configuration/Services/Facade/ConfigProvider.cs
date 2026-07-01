using Infrastructure.Configuration.Contracts;

namespace Infrastructure.Configuration.Services.Facade;

public class ConfigProvider
{
    private readonly ConfigFileReader _reader;
    private readonly ConfigJsonParser _parser;

    public ConfigProvider(ConfigFileReader reader, ConfigJsonParser parser)
    {
        _reader = reader;
        _parser = parser;
    }

    public Config Load(string path)
    {
        string json = _reader.Read(path);
        return _parser.Deserialize(json);
    }
}
