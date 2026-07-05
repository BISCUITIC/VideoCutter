using Application.Configuration.Contracts;
using Application.Configuration.Interfaces;
using Domain.Definitions;

namespace Application.Configuration.Services;

public class ConfigProvider
{
    private readonly IConfigReader _reader;
    private readonly IConfigParser _parser;
    private readonly IConfigMapper _mapper;

    public ConfigProvider(IConfigReader reader, 
                          IConfigParser parser, 
                          IConfigMapper mapper)
    {
        _reader = reader;
        _parser = parser;
        _mapper = mapper;
    }

    public VideoProcessingDefinition Load(string path)
    {
        string configContent = _reader.Read(path);

        ConfigContract configContract = _parser.Deserialize(configContent);
        foreach(var step in configContract.PipelineDefinition.Steps)
        {
            Console.WriteLine(step.Type + " " + step.Parameters);
        }
        VideoProcessingDefinition result = _mapper.Map(configContract);

        return result;
    }
}
