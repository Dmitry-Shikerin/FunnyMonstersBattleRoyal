using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Achievements.Domain.Configs;
using Sources.EcsBoundedContexts.AdvertisingAfterWaves.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain.Configs;
using Sources.EcsBoundedContexts.AnimatorLod.Domain.Configs;
using Sources.EcsBoundedContexts.BurnAbilities.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Configs;
using Sources.EcsBoundedContexts.Enemies.Domain.Configs;
using Sources.EcsBoundedContexts.Lights.Domain.Configs;
using Sources.EcsBoundedContexts.Upgrades.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs;
using Sources.Frameworks.MyGameCreator.SkyAndWeathers.Domain;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sources.Frameworks.GameServices.Prefabs.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(AddressablesAssetConfig), menuName = "Configs/" + nameof(AddressablesAssetConfig), order = 51)]
    public class AddressablesAssetConfig  : ScriptableObject
    {
        [field: Title("Configs")]
        [field: SerializeField] public AssetReferenceT<SkyAndWeatherCollector> SkyAndWeatherCollector { get; private set; }
        [field: SerializeField] public AssetReferenceT<AchievementConfigCollector> AchievementConfigCollector { get; private set; }
        [field: SerializeField] public AssetReferenceT<AnimatorLodSettingsCollector> AnimatorLodSettingsCollector { get; private set; }
        [field: SerializeField] public AssetReferenceT<ShadowManagerConfigCollector> ShadowManagerConfigCollector { get; private set; }
        [field: SerializeField] public AssetReferenceT<UpgradeConfigContainer> UpgradeConfigContainer { get; private set; }
        [field: SerializeField] public AssetReferenceT<CharacterRangeConfig> CharacterRangeConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<CharacterConfig> CharacterMeleeConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<EnemyConfig> EnemyConfig { get; private set; }        
        [field: SerializeField] public AssetReferenceT<EnemyKamikazeConfig> EnemyKamikazeConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<EnemyBossConfig> EnemyBossConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<UiConfig> UiConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<AnimationConfig> AnimationConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<AdvertisingAfterWaveConfig> AdvertisingAfterWaveConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<BurnConfig> BurnConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<DailyRewardConfig> DailyRewardConfig { get; private set; }
        
        [field: Title("Prefabs")]
        [field: SerializeField] public AssetReferenceT<GameObject> CharacterMeleeModule { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> CharacterRangeModule { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> EnemyModule { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> EnemyKamikazeModule { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> EnemyBossModule { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> NukeBombModule { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> FlamethrowerModule { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> ExplosionBodyBloodyModule { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> ExplosionBodyModule { get; private set; }
    }
}