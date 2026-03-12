using System;
using Leopotam.EcsProto;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Interfaces;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation
{
    public readonly struct Transition<TEnumState> : ITransition<TEnumState>
        where TEnumState : Enum
    {
        private readonly Func<ProtoEntity, bool> _condition;
        private readonly TEnumState _nextState;

        public Transition(TEnumState nextState, Func<ProtoEntity, bool> condition)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _nextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
        }

        public TEnumState GetNextState(ProtoEntity entity) =>
            _nextState;

        public bool CanTransit(ProtoEntity entity) =>
            _condition.Invoke(entity);
    }
}