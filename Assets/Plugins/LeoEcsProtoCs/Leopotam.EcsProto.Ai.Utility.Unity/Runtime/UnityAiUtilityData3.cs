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
    [CreateAssetMenu (menuName = "LeoECS Proto/UtilityAI/Данные с 3 параметрами")]
#endif
    public class UnityAiUtilityData3 : ScriptableObject {
        [SerializeField] string _name1;
        [SerializeField] AnimationCurve _curve1 = AnimationCurve.Constant (0f, 1f, 0f);
        [SerializeField] string _name2;
        [SerializeField] AnimationCurve _curve2 = AnimationCurve.Constant (0f, 1f, 0f);
        [SerializeField] string _name3;
        [SerializeField] AnimationCurve _curve3 = AnimationCurve.Constant (0f, 1f, 0f);
        [SerializeField] AiUtilityOperationType _operationType = AiUtilityOperationType.Average;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public float Evaluate (float t1, float t2, float t3) {
            var v1 = _curve1.Evaluate (t1);
            var v2 = _curve2.Evaluate (t2);
            var v3 = _curve3.Evaluate (t3);
            switch (_operationType) {
                case AiUtilityOperationType.Max:
                    return Mathf.Max (Mathf.Max (v1, v2), v3);
                case AiUtilityOperationType.Min:
                    return Mathf.Min (Mathf.Min (v1, v2), v3);
                case AiUtilityOperationType.Mul:
                    return v1 * v2 * v3;
                case AiUtilityOperationType.Add:
                    return v1 + v2 + v3;
                default:
                    return (v1 + v2 + v3) / 3f;
            }
        }
    }
}
