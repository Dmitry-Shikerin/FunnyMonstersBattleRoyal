using System.Collections.Generic;
using Sources.EcsBoundedContexts.Gizmoses.Domain;
using UnityEngine;

namespace Sources.BoundedContexts.Gizmoses.Presentation
{
    public class GizmosView : MonoBehaviour
    {
        [SerializeField] private bool _isDraw = true;
        [SerializeField] private List<DrawerContainer> _drawers = new ();

        private void OnDrawGizmos()
        {
            if (_isDraw == false)
                return;
            
            foreach (var drawer in _drawers)
            {
                if (drawer.Drawer.DrawType != GizmosDrawType.Default)
                    continue;
                
                drawer.Drawer.Draw(gameObject);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            if (_isDraw == false)
                return;
            
            foreach (var drawer in _drawers)
            {
                if (drawer.Drawer.DrawType != GizmosDrawType.Selected)
                    continue;
                
                drawer.Drawer.Draw(gameObject);
            }
        }
    }
}