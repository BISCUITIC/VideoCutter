using Application.Configuration.Contracts;

namespace Application.Configuration.Interfaces;

public interface IConfigParser
{
    ConfigContract Deserialize(string config);
}
