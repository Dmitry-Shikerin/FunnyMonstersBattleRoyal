using UnityEngine;

namespace Sources.BoundedContexts.Cursors.Services
{
    public interface ICursorService
    {
        void SetState(CursorLockMode lockMode);
        void LockCursor();
        void UnlockCursor();
    }
}