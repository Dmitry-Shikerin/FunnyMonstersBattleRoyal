using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Input.Infrastructure;

namespace Sources.EcsBoundedContexts.Input.Controllers
{
    [EcsSystem(15)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class InputInitializeSystem : IProtoInitSystem
    {
        private readonly InputEntityFactory _factory;

        public InputInitializeSystem(InputEntityFactory factory)
        {
            _factory = factory;
        }

        public void Init(IProtoSystems systems)
        {
            _factory.Create(null);
        }
    }
}