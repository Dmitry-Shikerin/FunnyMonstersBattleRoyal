using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins
{
    public class TailSkinView : MonoBehaviour
    {
        [field: SerializeField] public TailSkinName Name { get; private set; }
    }
}