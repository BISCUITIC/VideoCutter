using Infrastructure.Engine.FFmpeg.Models;

namespace Infrastructure.Engine.FFmpeg.Services.Interfaces;

public interface IFilterGraphSerializer
{
    string Serialize(FilterGraph filterGraph);
}
