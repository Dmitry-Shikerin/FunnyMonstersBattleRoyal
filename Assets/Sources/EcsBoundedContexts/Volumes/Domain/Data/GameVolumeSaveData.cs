using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Volumes.Domain.Data
{
    public struct GameVolumeSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public float Value { get; set; }
        public bool IsMuted { get; set; }
    }
}