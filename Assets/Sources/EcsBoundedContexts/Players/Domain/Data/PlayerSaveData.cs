using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Players.Domain.Data
{
    public struct PlayerSaveData : IEntitySaveData
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}