using Fusion;
using Sources.EcsBoundedContexts.Core;

namespace Sources.EcsBoundedContexts.NetworkCore.Services
{
    public class NetworkEcsRunner : SimulationBehaviour
    {
        public EcsGameStartUp EcsGameStartUp { get; } = new ();
        
        public override void FixedUpdateNetwork()
        {
            if (Runner.IsClient)
                return;
            
            EcsGameStartUp.Update(Runner.DeltaTime);
        }
    }
}