// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System.Runtime.CompilerServices;
using UnityEngine;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Ai.Utility.Unity {
#if UNITY_EDITOR
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    [CreateAssetMenu (menuName = "LeoECS Proto/UtilityAI/Данные с 1 параметром")]
#endif
    public class UnityAiUtilityData1 : ScriptableObject {
        [SerializeField] string _name1;
        [SerializeField] AnimationCurve _curve1 = AnimationCurve.Constant (0f, 1f, 0f);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public float Evaluate (float t1) => _curve1.Evaluate (t1);
    }
}
