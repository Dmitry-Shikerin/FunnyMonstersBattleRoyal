using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.ApplyAbility.Domain.Data
{
    public struct AbilitySaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public bool IsFirstUsedCompleted { get; set; }
    }
}