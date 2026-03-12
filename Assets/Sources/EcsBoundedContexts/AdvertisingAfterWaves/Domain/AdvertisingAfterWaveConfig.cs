using UnityEngine;

namespace Sources.EcsBoundedContexts.AdvertisingAfterWaves.Domain
{
    [CreateAssetMenu(
        fileName = nameof(AdvertisingAfterWaveConfig), 
        menuName = "Configs/" + nameof(AdvertisingAfterWaveConfig), 
        order = 51)]
    public class AdvertisingAfterWaveConfig : ScriptableObject
    {
        [field: SerializeField] public int WavesCount { get; private set; } = 20;
        [field: SerializeField] public int SecondsCount { get; private set; } = 3;
    }
}