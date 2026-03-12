using System;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs
{
    [Serializable]
    public abstract class WaveGenerationConfig
    {
        [SerializeField] public int TotalWaves;
        
        public abstract EnemySpawnerWave CreateWave(EnemySpawnerConfig config, int waveId);
    }
}