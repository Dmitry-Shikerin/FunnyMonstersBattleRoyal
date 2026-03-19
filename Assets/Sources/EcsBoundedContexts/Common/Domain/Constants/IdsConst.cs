using System.Collections.Generic;
using System.Linq;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Data;
using Sources.EcsBoundedContexts.KillEnemyCounters.Domain.Data;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Data;
using Sources.EcsBoundedContexts.Tutorials.Domain.Data;
using Sources.EcsBoundedContexts.Volumes.Domain.Data;
using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Common.Domain.Constants
{
    public class IdsConst
    {
        //Scenes
        public const string MainMenu = "MainMenu";
        public const string Gameplay = "Gameplay";
        
        //Common
        public const string MainCamera = "MainCamera";
        public const string Player = "Player";
        public const string EnemySpawner = "EnemySpawner";
        public const string Bunker = "Bunker";
        public const string ExplosionBodiesSpawner = "ExplosionBodiesSpawner";
        public const string PlayerWallet = "PlayerWallet";
        public const string KillEnemyCounter = "KillEnemyCounter";
        public const string Tutorial = "Tutorial";
        public const string DailyReward = "DailyReward";
        public const string HealthBooster = "HealthBuster";
        
        //Abilities
        public const string CharacterSpawnerAbility = "CharacterSpawnerAbility";
        public const string NukeAbility = "NukeAbility";
        public const string FlamethrowerAbility = "FlamethrowerAbility";
        public const string NukeBomb = "NukeBomb";

        //Upgrades
        public const string AttackUpgrade = "AttackUpgrade";
        public const string HealthUpgrade = "HealthUpgrade";
        public const string FlamethrowerUpgrade = "FlamethrowerUpgrade";
        public const string NukeUpgrade = "NukeUpgrade";

        //Settings
        public const string SoundsVolume = "SoundsVolume";
        public const string MusicVolume = "MusicVolume";
        
        //Achievements
        public const string FirstEnemyKillAchievement = "FirstEnemyKillAchievement";
        public const string FirstUpgradeAchievement = "FirstUpgradeAchievement";
        public const string FirstHealthBoosterUsageAchievement = "FirstHealthBoosterUsageAchievement";
        public const string FirstWaveCompletedAchievement = "FirstWaveCompletedAchievement";
        public const string ScullsDiggerAchievement = "ScullsDiggerAchievement";
        public const string MaxUpgradeAchievement = "MaxUpgradeAchievement";
        public const string FiftyWaveCompletedAchievement = "FiftyWaveCompletedAchievement";
        public const string AllAbilitiesUsedAchievement = "AllAbilitiesUsedAchievement";
        public const string CompleteGameWithOneHealthAchievement = "CompleteGameWithOneHealthAchievement";
        
        private static Dictionary<string, List<string>> _cachedIdsByType = new ();

        public static IReadOnlyDictionary<string, EntityData> AllIds { get; } = new Dictionary<string, EntityData>()
        {
            [DailyReward] = new (DailyReward, typeof(DailyRewardSaveData), false),
            [PlayerWallet] = new (PlayerWallet, typeof(PlayerWalletSaveData), true),
            [KillEnemyCounter] = new (KillEnemyCounter, typeof(KillEnemyCounterSaveData), true),
            [Tutorial] = new (Tutorial, typeof(TutorialSaveData), false),
            [SoundsVolume] = new (SoundsVolume, typeof(GameVolumeSaveData), false),
            [MusicVolume] = new (MusicVolume, typeof(GameVolumeSaveData), false),
        };

        public static IReadOnlyList<string> GetIds<T>() 
            where T : struct, IEntitySaveData
        {
            string id = typeof(T).Name;
            
            if (_cachedIdsByType.TryGetValue(id, out List<string> ids))
                return ids;
            
            _cachedIdsByType[id] = AllIds.Values
                .Where(data => data.Type == typeof(T))
                .Select(data => data.ID)
                .ToList();
            
            return _cachedIdsByType[id];
        }

        public static IReadOnlyList<string> GetDeleteIds()
        {
            string id = "Deleted";
            
            if (_cachedIdsByType.TryGetValue(id, out List<string> ids))
                return ids;
            
            _cachedIdsByType[id] = AllIds.Values
                .Where(data => data.IsDeleted)
                .Select(data => data.ID)
                .ToList();
            
            return _cachedIdsByType[id];
        }
        
        public static IReadOnlyList<string> GetAll()
        {
            string id = "All";
            
            if (_cachedIdsByType.TryGetValue(id, out List<string> ids))
                return ids;
            
            _cachedIdsByType[id] = AllIds.Keys.ToList();
            
            return _cachedIdsByType[id];
        }
    }
}