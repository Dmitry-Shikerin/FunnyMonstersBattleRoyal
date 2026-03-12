// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using Leopotam.EcsProto.QoL;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Ai.Utility {
    public interface IAiUtilitySolver {
        float Solve (ProtoEntity entity);
        void Apply (ProtoEntity entity);
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    sealed class AiUtilitySystem : IProtoRunSystem {
        [DI] readonly AiUtilityAspect _aspect = default;
        readonly IAiUtilitySolver[] _solvers;

        public AiUtilitySystem (IAiUtilitySolver[] solvers) {
            _solvers = solvers;
        }

        public void Run () {
            foreach (var entity in _aspect.RequestIt) {
                ref var res = ref _aspect.ResponseEvent.GetOrAdd (entity, out _);
                _aspect.RequestEvent.Del (entity);
                foreach (var solver in _solvers) {
                    var priority = solver.Solve (entity);
                    if (priority > res.Priority) {
                        res.Priority = priority;
                        res.Solver = solver;
                    }
                }
            }
        }
    }
}
