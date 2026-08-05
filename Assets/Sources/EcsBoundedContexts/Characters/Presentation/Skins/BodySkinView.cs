using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins
{
    public class BodySkinView : MonoBehaviour
    {
        [field: SerializeField] public BodySkinName Name { get; private set; }
    }
}