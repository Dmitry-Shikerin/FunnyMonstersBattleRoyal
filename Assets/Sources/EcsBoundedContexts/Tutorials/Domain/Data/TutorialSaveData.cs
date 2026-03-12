using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Tutorials.Domain.Data
{
    public struct TutorialSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}