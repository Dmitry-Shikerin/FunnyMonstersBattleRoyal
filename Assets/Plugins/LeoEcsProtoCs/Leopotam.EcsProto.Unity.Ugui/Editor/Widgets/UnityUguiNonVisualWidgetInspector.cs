// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEditor;
using UnityEditor.UI;

namespace Leopotam.EcsProto.Unity.Ugui.Editor {
    [CustomEditor (typeof (UnityUguiNonVisualWidget), false)]
    [CanEditMultipleObjects]
    sealed class UnityUguiNonVisualWidgetInspector : GraphicEditor {
        public override void OnInspectorGUI () {
            serializedObject.Update ();
            EditorGUILayout.PropertyField (m_Script);
            RaycastControlsGUI ();
            serializedObject.ApplyModifiedProperties ();
        }
    }
}
