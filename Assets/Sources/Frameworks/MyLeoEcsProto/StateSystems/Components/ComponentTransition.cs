using System;
using Leopotam.EcsProto;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Components
{
    public readonly struct ComponentTransition<T> : IComponentTransition
        where T : struct, IStateComponent
    {
        private readonly Func<bool> _condition;
        private readonly ProtoPool<T> _pool;

        public ComponentTransition(Func<bool> condition, ProtoPool<T> pool)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }

        public void Transit(ProtoEntity entity) =>
            _pool.Add(entity);

        public bool CanTransit(ProtoEntity entity) =>
            _condition.Invoke();
    }
}