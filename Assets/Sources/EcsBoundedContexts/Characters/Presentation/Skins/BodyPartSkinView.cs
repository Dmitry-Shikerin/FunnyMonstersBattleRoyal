using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins
{
    public class BodyPartSkinView : MonoBehaviour
    {
        [field: SerializeField] public BodyPartSkinName Name { get; private set; }
    }
}