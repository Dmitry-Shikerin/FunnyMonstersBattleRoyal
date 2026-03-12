using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Core;

namespace Sources.EcsBoundedContexts.Particles.Presentation
{
    public class ReturnToPoolParticleModule : EntityModule
    {
        private void OnParticleSystemStopped() =>
            Entity.GetReturnToPoolAction().ReturnToPool.Invoke();
    }
}