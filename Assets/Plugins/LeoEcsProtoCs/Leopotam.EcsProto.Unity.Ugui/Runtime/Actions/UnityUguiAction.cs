// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System.Runtime.CompilerServices;
using UnityEngine;

namespace Leopotam.EcsProto.Unity.Ugui {
    [DefaultExecutionOrder (100)]
    public abstract class UnityUguiAction<T> : ProtoUnityAction<T> where T : struct {
        [SerializeField] UnityEngine.UI.Selectable _selectable;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        protected override bool IsValidForEvent () {
            return ProtoUnityWorlds.Connected () && (!_selectable || _selectable.interactable);
        }
    }
}
