using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Tail
{
    public class TailSkinView : MonoBehaviour
    {
        [field: SerializeField] public TailSkinName Name { get; private set; }
    }
}