using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins
{
    public class EyeSkinView : MonoBehaviour
    {
        [field: SerializeField] public EyeSkinName Name { get; private set; }
    }
}