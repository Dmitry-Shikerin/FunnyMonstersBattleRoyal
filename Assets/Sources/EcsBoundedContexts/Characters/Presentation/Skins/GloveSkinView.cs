using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins
{
    public class GloveSkinView : MonoBehaviour
    {
        [field: SerializeField] public GloveSkinName Name { get; private set; }
    }
}