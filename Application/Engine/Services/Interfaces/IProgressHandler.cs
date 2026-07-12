namespace Application.Engine.Services.Interfaces;

public interface IProgressHandler
{
    void Handle(int segmentIndex, double percentage);
}
