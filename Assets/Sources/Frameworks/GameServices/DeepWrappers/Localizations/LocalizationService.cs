using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Infrastructure.Services;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;
using UnityEngine;
using YG;

namespace Sources.Frameworks.GameServices.DeepWrappers.Localizations
{
    public class LocalizationService : ILocalizationService
    {
        public void Translate()
        {
            // string key = WebApplication.IsRunningOnWebGL
            //     ? YG2.envir.language
            //     : GetEditorKey();

            DeepLocalizationBrain.Translate("key");
        }

        public string GetText(string key) =>
            DeepLocalizationBrain.GetText(key);

        public Sprite GetSprite(string key) =>
            DeepLocalizationBrain.GetSprite(key);
        
        private string GetEditorKey()
        {
            return LocalizationDataBase.Instance.Language switch
            {
                LocalizationLanguage.English => LocalizationConst.English,
                LocalizationLanguage.Turkish => LocalizationConst.Turkish,
                LocalizationLanguage.Russian => LocalizationConst.Russian,
                _ => LocalizationConst.English,
            };
        }
    }
}