using Sources.EcsBoundedContexts.Settings.Domain.Enums;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Infrastructure.Services
{
    public class QualityService : IQualityService
    {
        public void SetQualityLevel(GraphicsQualities quality)
        {
            QualitySettings.SetQualityLevel((int)quality, true);
        }

        public void EnableVSync(bool isEnabled)
        {
            QualitySettings.vSyncCount = isEnabled ? 1 : 0;
        }
    }
}