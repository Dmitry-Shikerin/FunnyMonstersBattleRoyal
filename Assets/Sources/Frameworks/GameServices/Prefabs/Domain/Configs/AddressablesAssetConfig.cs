using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.AdvertisingAfterWaves.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain.Configs;
using Sources.EcsBoundedContexts.AnimatorLod.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sources.Frameworks.GameServices.Prefabs.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(AddressablesAssetConfig), menuName = "Configs/" + nameof(AddressablesAssetConfig), order = 51)]
    public class AddressablesAssetConfig  : ScriptableObject
    {
        [field: Title("Configs")]
        [field: SerializeField] public AssetReferenceT<CharacterConfig> CharacterMeleeConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<UiConfig> UiConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<AnimationConfig> AnimationConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<AdvertisingAfterWaveConfig> AdvertisingAfterWaveConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<DailyRewardConfig> DailyRewardConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<AnimatorLodSettingsCollector> AnimatorLodConfig { get; set; }


        [field: Title("Prefabs")]
        [field: SerializeField] public AssetReferenceT<GameObject> CharacterModule { get; private set; }
    }
}