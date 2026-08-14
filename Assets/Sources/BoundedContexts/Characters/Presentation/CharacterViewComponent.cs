using Fusion;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation
{
    public class CharacterViewComponent : MonoBehaviour, IViewComponent
    {
        public PlayerRef PlayerRef { get; private set; }
        
        public void Init(PlayerRef playerRef) => 
            PlayerRef = playerRef;
    }
}