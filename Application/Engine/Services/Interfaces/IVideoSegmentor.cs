using Domain.Definitions;
using Domain.Processing;

namespace Application.Engine.Services.Interfaces;

public interface IVideoSegmentor
{
    IReadOnlyCollection<VideoSegment> Process(VideoSegmentation segmentation);
}
