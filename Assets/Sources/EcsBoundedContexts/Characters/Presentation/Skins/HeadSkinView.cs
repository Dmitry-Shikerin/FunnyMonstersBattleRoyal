using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins
{
    public class HeadSkinView : MonoBehaviour
    {
        [field: SerializeField] public HeadSkinName Name { get; private set; }
    }
}