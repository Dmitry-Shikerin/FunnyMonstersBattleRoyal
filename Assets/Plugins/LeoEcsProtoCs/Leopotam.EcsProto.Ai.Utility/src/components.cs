// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

namespace Leopotam.EcsProto.Ai.Utility {
    public struct AiUtilityResponseEvent {
        public float Priority;
        public IAiUtilitySolver Solver;
    }

    struct AiUtilityRequestEvent { }
}
