using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepCores.Domain;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(EnemySpawnerWave), menuName = "Configs/" + nameof(EnemySpawnerWave), order = 51)]
    public class EnemySpawnerWave : ScriptableObject
    {
        private const int Space = 10;
        private const string WaveHeader = "Wave";
        private const string EnemyHeader = "Enemy";
        private const string BossHeader = "Boss";
        private const string KamikazeHeader = "Kamikaze";
        
        [EnumToggleButtons] 
        [HideLabel] 
        [UsedImplicitly]
        [SerializeField] private EnableState enableState = EnableState.Off;
        [EnableIf("enableState", EnableState.On)]
        [SerializeField] private EnemySpawnerConfig _parent;
        [EnableIf("enableState", EnableState.On)]
        [field: SerializeField] public int WaveId { get; private set; }
        [field: SerializeField] public int MoneyPerRevivalCharacters { get; private set; }
        [field: Header(WaveHeader)]
        [field: Space(Space)] 
        [field: SerializeField] public int SpawnDelay { get; set; }
        [field: Header(EnemyHeader)] 
        [field: SerializeField] public int EnemyCount { get; set; }
        [field: Header(BossHeader)]
        [field: SerializeField] public int BossesCount { get; set; }
        [field: Header(KamikazeHeader)]
        [field: SerializeField] public int KamikazeEnemyCount { get; set; }

        public EnemySpawnerConfig Parent
        {
            get => _parent; 
            set => _parent = value;
        }
        
        public void SetWaveId(int id) =>
            WaveId = id;
        
#if UNITY_EDITOR
        [Button(ButtonSizes.Medium)] [PropertySpace(20)]
        private void Remove() =>
            Parent.RemoveWave(this);
#endif
    }
}