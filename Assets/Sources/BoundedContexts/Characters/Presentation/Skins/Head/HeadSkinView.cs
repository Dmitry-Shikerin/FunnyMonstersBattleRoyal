using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Head
{
    public class HeadSkinView : MonoBehaviour
    {
        [field: SerializeField] public HeadSkinName Name { get; private set; }
    }
}