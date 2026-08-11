using Sources.EcsBoundedContexts.Settings.Domain.Enums;

namespace Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces
{
    public interface IQualityService
    {
        void SetQualityLevel(GraphicsQualities quality);
        void EnableVSync(bool isEnabled);
    }
}