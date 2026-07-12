using Domain.Definitions;

namespace Application.Configuration.Interfaces;

public interface IConfigProvider
{
    public VideoProcessing Load(string path);
}
