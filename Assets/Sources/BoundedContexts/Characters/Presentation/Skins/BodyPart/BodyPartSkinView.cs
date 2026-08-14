using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.BodyPart
{
    public class BodyPartSkinView : MonoBehaviour
    {
        [field: SerializeField] public BodyPartSkinName Name { get; private set; }
    }
}