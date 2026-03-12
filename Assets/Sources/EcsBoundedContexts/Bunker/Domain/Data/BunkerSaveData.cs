using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Bunker.Domain.Data
{
    public struct BunkerSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public int Health { get; set; }
    }
}