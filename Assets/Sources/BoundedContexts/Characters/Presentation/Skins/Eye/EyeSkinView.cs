using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Eye
{
    public class EyeSkinView : MonoBehaviour
    {
        [field: SerializeField] public EyeSkinName Name { get; private set; }
    }
}