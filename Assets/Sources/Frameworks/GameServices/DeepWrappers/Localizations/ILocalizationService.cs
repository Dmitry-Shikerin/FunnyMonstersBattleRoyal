using UnityEngine;

namespace Sources.Frameworks.GameServices.DeepWrappers.Localizations
{
    public interface ILocalizationService
    {
        void Translate();
        string GetText(string key);
        Sprite GetSprite(string key);
    }
}