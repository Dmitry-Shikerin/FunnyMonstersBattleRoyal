// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEditor;

namespace Leopotam.EcsProto.Unity.Editor.Inspectors {
    sealed class LongInspector : ProtoComponentInspector<long> {
        protected override bool OnRender (string label, ref long value) {
            var newValue = EditorGUILayout.LongField (label, value);
            if (newValue == value) { return false; }
            value = newValue;
            return true;
        }
    }
}
