using Fusion;
using Sources.EcsBoundedContexts.Core;

namespace Sources.EcsBoundedContexts.NetworkCore.Services
{
    public class NetworkEcsRunner : SimulationBehaviour
    {
        public IEcsGameStartUp LeoGameStartUp { get; } = new LeoGameStartUp();
        
        public override void FixedUpdateNetwork()
        {
            if (Runner.IsClient)
                return;
            
            LeoGameStartUp.Update(Runner.DeltaTime);
        }
    }
}