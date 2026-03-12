using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Achievements.Domain.Data
{
    public struct AchievementSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}