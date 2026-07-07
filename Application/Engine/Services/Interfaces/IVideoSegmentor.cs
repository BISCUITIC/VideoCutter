using Domain.Definitions;
using Domain.Engine;

namespace Application.Engine.Services.Interfaces;

public interface IVideoSegmentor
{
    IReadOnlyCollection<VideoSegment> Process(VideoSegmentation segmentation);
}
