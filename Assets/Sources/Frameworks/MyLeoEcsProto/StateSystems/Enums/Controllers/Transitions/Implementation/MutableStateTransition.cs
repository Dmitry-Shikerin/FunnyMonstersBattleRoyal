using System;
using Leopotam.EcsProto;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Interfaces;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation
{
    public readonly struct MutableStateTransition<TEnumState> : ITransition<TEnumState>
        where TEnumState : Enum
    {
        private readonly Func<ProtoEntity, TEnumState> _nextState;
        private readonly Func<ProtoEntity, bool> _condition;

        public MutableStateTransition(Func<ProtoEntity, TEnumState> nextState, Func<ProtoEntity, bool> condition)
        {
            _nextState = nextState;
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public TEnumState GetNextState(ProtoEntity entity) =>
            _nextState.Invoke(entity);

        public bool CanTransit(ProtoEntity entity) =>
            _condition.Invoke(entity);
    }
}