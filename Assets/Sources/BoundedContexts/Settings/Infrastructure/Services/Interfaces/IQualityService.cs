using Sources.BoundedContexts.Settings.Domain.Enums;

namespace Sources.BoundedContexts.Settings.Infrastructure.Services.Interfaces
{
    public interface IQualityService
    {
        void SetQualityLevel(GraphicsQualities quality);
        void EnableVSync(bool isEnabled);
    }
}