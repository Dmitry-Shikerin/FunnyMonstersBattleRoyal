using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Dictionaries;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Enums;
using UnityEditor;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(EnemySpawnerConfig), menuName = "Configs/" + nameof(EnemySpawnerConfig),
        order = 51)]
    public class EnemySpawnerConfig : ScriptableObject
    {
        private const string DataHeader = "Data";
        private const string EnemyHeader = "Enemy";
        private const string BossHeader = "Boss";
        private const string KamikazeHeader = "Kamikaze";
        private const string SpawnWeightsHeader = "SpawnWeights";
        private const int Space = 5;

        [Header(DataHeader)] [DisableInEditorMode] [SerializeField]
        private List<EnemySpawnerWave> _waves;

        public IReadOnlyList<EnemySpawnerWave> Waves => _waves;

        [field: Header(EnemyHeader)]
        [field: SerializeField] public int StartEnemyAttackPower { get; private set; } = 5;
        [field: SerializeField] public int AddedEnemyAttackPower { get; private set; } = 1;
        [field: Space(Space)]
        [field: SerializeField] public int StartEnemyHealth { get; private set; } = 20;
        [field: SerializeField] public int AddedEnemyHealth { get; private set; } = 1;

        [field: Header(BossHeader)]
        [field: SerializeField] public int StartBossAttackPower { get; private set; } = 5;
        [field: SerializeField] public int AddedBossAttackPower { get; private set; } = 1;
        [field: Space(Space)]
        [field: SerializeField] public int StartBossMassAttackPower { get; private set; } = 10;
        [field: SerializeField] public int AddedBossMassAttackPower { get; private set; } = 1;
        [field: Space(Space)]
        [field: SerializeField] public int StartBossHealth { get; private set; } = 80;
        [field: SerializeField] public int AddedBossHealth { get; private set; } = 20;

        [field: Header(KamikazeHeader)]
        [field: SerializeField] public int StartKamikazeHealth { get; private set; } = 25;
        [field: SerializeField] public int AddedKamikazeHealth { get; private set; } = 1;
        [field: Space(Space)]
        [field: SerializeField] public int StartKamikazeAttackPower { get; private set; } = 2;
        [field: SerializeField] public int AddedKamikazeAttackPower { get; private set; } = 1;
        [field: Space(Space)]
        [field: SerializeField] public int StartKamikazeMassAttackPower { get; private set; } = 4;
        [field: SerializeField] public int AddedKamikazeMassAttackPower { get; private set; } = 1;

        [field: Header(SpawnWeightsHeader)]
        [field: SerializeField] public SpawnLogic SpawnLogic { get; private set; } = SpawnLogic.Weighted;
        [field: SerializeField] public WeightsDictionary Weights { get; private set; }

        [field: Header("Configs Generator")]
        [OnValueChanged("OnWaveGeneratorTypeChanged")]
        [SerializeField] private WaveGeneratorType _waveGeneratorType;
        [field: ShowIf(nameof(IsShow))]
        [field: SerializeReference] public WaveGenerationConfig WaveGenerationConfig { get; private set; }

        private bool IsShow()
        {
            return WaveGenerationConfig != null;
        }

        private void OnWaveGeneratorTypeChanged(WaveGeneratorType type)
        {
            WaveGenerationConfig = type switch
            {
                WaveGeneratorType.PillarProgression => new WaveGenerationPillarProgressionConfig(),
                _ => null
            };
        }
        //TODO добавить стратегии для спавна
        //TODO пробежаться по всем конфигам и вынести подобную логику в ютилс
#if UNITY_EDITOR
        [Button]
        public void AutoGenerateWaves()
        {
            for (int waveId = 1; waveId <= WaveGenerationConfig.TotalWaves; waveId++)
            {
                var wave = WaveGenerationConfig.CreateWave(this, waveId);
                wave.Parent = this;
                AssetDatabase.AddObjectToAsset(wave, this);
                wave.SetWaveId(waveId);
                wave.name = $"Wave_{waveId}";
                _waves.Add(wave);
            }

            AssetDatabase.SaveAssets();
        }

        [Button]
        public void RemoveAllWaves()
        {
            for (int i = _waves.Count - 1; i >= 0; i--)
            {
                EnemySpawnerWave wave = _waves[i];
                AssetDatabase.RemoveObjectFromAsset(wave);
                _waves.Remove(wave);
            }

            AssetDatabase.SaveAssets();
        }

        public void RemoveWave(EnemySpawnerWave wave)
        {
            AssetDatabase.RemoveObjectFromAsset(wave);
            _waves.Remove(wave);
            RenameWaves();
            AssetDatabase.SaveAssets();
        }

        [Button]
        private void RenameWaves()
        {
            for (int i = 0; i < _waves.Count; i++)
            {
                _waves[i].name = $"Wave_{i + 1}";
                _waves[i].SetWaveId(i + 1);
            }

            AssetDatabase.SaveAssets();
        }

        [UsedImplicitly]
        [ResponsiveButtonGroup("Buttons")]
        private void CreateWave()
        {
            EnemySpawnerWave wave = CreateInstance<EnemySpawnerWave>();
            int waveId = _waves.Count + 1;
            wave.Parent = this;
            AssetDatabase.AddObjectToAsset(wave, this);
            wave.SetWaveId(waveId);
            wave.name = $"Wave_{waveId}";
            _waves.Add(wave);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}