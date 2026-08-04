using Fusion;
using Sources.EcsBoundedContexts.Core;

namespace Sources.EcsBoundedContexts.NetworkCore.Services
{
    public class NetworkEcsRunner : SimulationBehaviour
    {
        public LeoEcsGameStartUp LeoEcsGameStartUp { get; } = new ();
        
        public override void FixedUpdateNetwork()
        {
            if (Runner.IsClient)
                return;
            
            LeoEcsGameStartUp.Update(Runner.DeltaTime);
        }
    }
}