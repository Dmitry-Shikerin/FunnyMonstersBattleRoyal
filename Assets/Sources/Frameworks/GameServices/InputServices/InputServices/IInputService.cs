using UnityEngine;

namespace Sources.Frameworks.GameServices.InputServices.InputServices
{
    public interface IInputService
    {
        Vector2 MovementInput { get; }
        bool IsJumpPerformed { get; }
        
        void Initialize();
        void Destroy();
    }
}