using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers
{
    public abstract class EnumStateSystem<TEnumState, TComponent> : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
        where TEnumState : Enum
        where TComponent : struct, IEnumStateComponent<TEnumState>    
    {
        private readonly List<ITransition<TEnumState>> _transitions = new ();

        protected EnumStateSystem(ProtoWorld wold)
        {
            Pool = wold.Pool<TComponent>() as ProtoPool<TComponent>;
        }

        protected abstract ProtoIt ProtoIt { get;  }
        protected ProtoPool<TComponent> Pool { get; private set; }

        public virtual void Init(IProtoSystems systems)
        {
        }

        protected abstract bool IsState(ProtoEntity entity);
        
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

                if (IsState(entity) == false)
                    continue;
                
                if (state.IsEntered == false)
                {
                    Enter(entity);
                    state.IsEntered = true;
                }
                
                Update(entity);
                
                if (TryChangeState(entity, out TEnumState nextState) == false)
                    continue;
                
                Exit(entity);
                state.IsEntered = false;
                state.State = nextState;
            }
        }

        protected void AddTransition(ITransition<TEnumState> transition) =>
            _transitions.Add(transition);

        private bool TryChangeState(ProtoEntity entity, out TEnumState nextState)
        {
            foreach (ITransition<TEnumState> transition in _transitions)
            {
                if (transition.CanTransit(entity) == false)
                    continue;
                
                nextState = transition.GetNextState(entity);
                return true;
            }

            nextState = default;
            
            return false;
        }

        public virtual void Destroy()
        {
        }
    }
}