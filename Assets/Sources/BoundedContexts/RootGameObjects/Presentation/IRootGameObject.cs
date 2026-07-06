using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;

namespace Sources.BoundedContexts.RootGameObjects.Presentation
{
    public interface IRootGameObject
    {
        EntityLink MainCamera { get; }
    }
}