using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Components
{
    public abstract class ComponentStateSystem<TComponent> : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
        where TComponent : struct, IStateComponent
    {
        private readonly List<IComponentTransition> _transitions = new ();

        protected abstract ProtoIt ProtoIt { get;  }
        protected abstract ProtoPool<TComponent> Pool { get;  }

        public virtual void Init(IProtoSystems systems)
        {
        }
        
        protected virtual void Enter(ProtoEntity entity)
        {
        }

        protected abstract void Update(ProtoEntity entity);

        protected virtual void Exit(ProtoEntity entity)
        {
        }

        public void Run()
        {
            foreach (ProtoEntity entity in ProtoIt)
            {
                ref TComponent state = ref Pool.Get(entity);
                
                if (state.IsEntered == false)
                {
                    Enter(entity);
                    state.IsEntered = true;
                }
                
                Update(entity);
                
                if (TryChangeState(entity, out IComponentTransition targetTransition) == false)
                    continue;
                
                Exit(entity);
                Pool.Del(entity);
                targetTransition.Transit(entity);
            }
        }

        protected void AddTransition(IComponentTransition transition) =>
            _transitions.Add(transition);

        private bool TryChangeState(ProtoEntity entity, out IComponentTransition targetTransition)
        {
            foreach (IComponentTransition transition in _transitions)
            {
                if (transition.CanTransit(entity) == false)
                    continue;
                
                targetTransition = transition;
                
                return true;
            }

            targetTransition = default;
            
            return false;
        }

        public virtual void Destroy()
        {
        }
    }
}