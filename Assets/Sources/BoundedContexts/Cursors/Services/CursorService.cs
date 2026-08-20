using UnityEngine;

namespace Sources.BoundedContexts.Cursors.Services
{
    public class CursorService : ICursorService
    {
        private const string TexturePath = "Cursors/Cursor";
        
        public CursorService()
        {
            Texture2D cursorTexture = Resources.Load<Texture2D>(TexturePath);
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        public void SetState(CursorLockMode lockMode)
        {
            Cursor.lockState = lockMode;
        }

        public void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}