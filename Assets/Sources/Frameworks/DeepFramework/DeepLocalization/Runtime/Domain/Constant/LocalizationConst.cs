namespace Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant
{
    public class LocalizationConst
    {
        public const string Turkish = "tr";
        public const string Russian = "ru";
        public const string English = "en";

        public const string LocalizationDatabaseAsset = "t:LocalizationDataBase";
        public const string LocalizationPhraseAsset = "t:LocalizationPhrase";
        public const string LocalizationScopeAsset = "t:LocalizationScope";

        public const string LocalisationPhraseAssetPath =
            "Assets/Resources/Services/Localizations/Phrases/LocalizationPhrase.asset";
        public const string LocalizationDataBaseAssetPath = 
            "Services/Localizations/LocalizationDataBase";
        
        //Labels
        public const string UiLocalizationTextLabel = "<size=18><b><color=#FF7F50><i>UiLocalizationText</i></color></b></size>";
        public const string UiLocalizationSpriteLabel = "<size=18><b><color=#FF7F50><i>UiLocalizationSprite</i></color></b></size>";
        public const string UiLocalizationPhraseLabel = "<size=18><b><color=#FF7F50><i>UiLocalizationPhrase</i></color></b></size>";
        public const string UiLocalizationDataBaseLabel = "<size=18><b><color=#FF7F50><i>UiLocalizationDataBase</i></color></b></size>";
        
        //Editor
        public const string LocalizationMenuItem = "Tools/DeepFramework/DeepLocalisation";
    }
}