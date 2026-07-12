namespace Application.Engine.Services.Interfaces;

public interface IProgressHandler
{
    void Start(int totalSegments);

    void Handle(int segmentIndex, double percentage);

    void Finish();
}
