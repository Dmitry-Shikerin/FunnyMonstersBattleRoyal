using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Glove
{
    public class GloveSkinView : MonoBehaviour
    {
        [field: SerializeField] public GloveSkinName Name { get; private set; }
    }
}