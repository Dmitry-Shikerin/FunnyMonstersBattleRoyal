// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.EcsProto.Unity.Ugui {
    [RequireComponent (typeof (CanvasRenderer))]
    [RequireComponent (typeof (RectTransform))]
    public class UnityUguiNonVisualWidget : Graphic {
        public override void SetMaterialDirty () { }
        public override void SetVerticesDirty () { }
        public override Material material { get => defaultMaterial; set { } }
        public override void Rebuild (CanvasUpdate update) { }
    }
}
