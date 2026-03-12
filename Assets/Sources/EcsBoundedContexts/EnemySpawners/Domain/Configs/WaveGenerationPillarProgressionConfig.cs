using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class WaveGenerationPillarProgressionConfig : WaveGenerationConfig
    {
        [Header("Базовая сложность")] 
        [SerializeField] public int BaseEnemyCount = 10;

        [SerializeField] public int BaseBossCount = 1;
        [SerializeField] public int BaseKamikazeCount = 2;

        [Header("Прогрессия")] 
        [SerializeField] public float EnemyGrowthRate = 1.2f; // 20% рост за волну

        [SerializeField] public float BossGrowthRate = 1.1f;
        [SerializeField] public float KamikazeGrowthRate = 1.15f;

        [Header("Циклы сброса")] 
        [SerializeField] public int ResetCycleLength = 5; // Каждые 5 волн сброс

        [SerializeField] 
        [Range(0f, 1f)] public float ResetStrength = 0.3f; // На 30% откат
        [SerializeField] public int BreathingRoomWaves = 2; // 2 волны "отдыха"

        [Header("Вариативность")] 
        [Range(0f, 0.3f)]
        [SerializeField] public float RandomVariance = 0.1f; // 10% случайности
        
        public override EnemySpawnerWave CreateWave(EnemySpawnerConfig config, int waveId)
        {
            var wave = ScriptableObject.CreateInstance<EnemySpawnerWave>();
            wave.Parent = config;
            wave.SetWaveId(waveId);

            // Вычисляем базовые значения для волны
            var (enemyCount, bossCount, kamikazeCount) = CalculateWaveValues(waveId);

            // Применяем случайную вариативность
            enemyCount = ApplyVariance(enemyCount, RandomVariance);
            bossCount = ApplyVariance(bossCount, RandomVariance);
            kamikazeCount = ApplyVariance(kamikazeCount, RandomVariance);

            // Гарантируем минимальные значения
            wave.EnemyCount = Mathf.Max(1, enemyCount);
            wave.BossesCount = Mathf.Max(0, bossCount);
            wave.KamikazeEnemyCount = Mathf.Max(0, kamikazeCount);

            // Настройка задержки спавна (может уменьшаться с ростом сложности)
            wave.SpawnDelay = CalculateSpawnDelay(waveId);

            // Награда за волну (растет со сложностью)
            //todo ПОТОМ ДОРАБОТАТЬ
            //wave.MoneyPerRevivalCharacters = CalculateReward(waveId, genConfig);

            return wave;
        }

        private (int enemyCount, int bossCount, int kamikazeCount) CalculateWaveValues(int waveId)
        {
            // Определяем позицию в цикле сброса
            int cyclePosition = (waveId - 1) % ResetCycleLength;
            bool isBreathingRoom = cyclePosition >= ResetCycleLength - BreathingRoomWaves;

            // Базовый рост (экспоненциальный)
            float growthFactor = Mathf.Pow(EnemyGrowthRate, waveId - 1);
            float bossGrowthFactor = Mathf.Pow(BossGrowthRate, waveId - 1);
            float kamikazeGrowthFactor = Mathf.Pow(KamikazeGrowthRate, waveId - 1);

            // Применяем сброс сложности для "зон отдыха"
            if (isBreathingRoom)
            {
                float resetMultiplier = 1f - ResetStrength;
                growthFactor *= resetMultiplier;
                bossGrowthFactor *= resetMultiplier;
                kamikazeGrowthFactor *= resetMultiplier;
            }

            // Вычисляем значения
            int enemyCount = Mathf.RoundToInt(BaseEnemyCount * growthFactor);
            int bossCount = Mathf.RoundToInt(BaseBossCount * bossGrowthFactor);
            int kamikazeCount = Mathf.RoundToInt(BaseKamikazeCount * kamikazeGrowthFactor);

            return (enemyCount, bossCount, kamikazeCount);
        }

        private int ApplyVariance(int value, float variance)
        {
            float randomFactor = 1f + UnityEngine.Random.Range(-variance, variance);
            return Mathf.RoundToInt(value * randomFactor);
        }

        private int CalculateSpawnDelay(int waveId)
        {
            // Уменьшаем задержку с ростом сложности, но не ниже минимума
            int baseDelay = 2000; // мс
            int minDelay = 500;

            // В зонах отдыха даем больше времени
            int cyclePosition = (waveId - 1) % ResetCycleLength;
            bool isBreathingRoom = cyclePosition >= ResetCycleLength - BreathingRoomWaves;

            if (isBreathingRoom)
                return baseDelay;

            // Постепенное уменьшение задержки
            float reduction = Mathf.Pow(0.95f, waveId - 1);
            return Mathf.Max(minDelay, Mathf.RoundToInt(baseDelay * reduction));
        }

        private int CalculateReward(int waveId, WaveGenerationPillarProgressionConfig genPillarProgressionConfig)
        {
            int baseReward = 100;
            float growth = Mathf.Pow(1.15f, waveId - 1); // Награда растет быстрее сложности

            // В зонах отдыха награда немного меньше
            int cyclePosition = (waveId - 1) % genPillarProgressionConfig.ResetCycleLength;
            bool isBreathingRoom = cyclePosition >= genPillarProgressionConfig.ResetCycleLength - genPillarProgressionConfig.BreathingRoomWaves;

            if (isBreathingRoom)
                growth *= 0.8f;

            return Mathf.RoundToInt(baseReward * growth);
        }

    }
}