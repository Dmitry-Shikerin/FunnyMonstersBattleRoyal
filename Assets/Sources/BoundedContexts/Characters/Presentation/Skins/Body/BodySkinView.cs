using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Body
{
    public class BodySkinView : MonoBehaviour
    {
        [field: SerializeField] public BodySkinName Name { get; private set; }
    }
}