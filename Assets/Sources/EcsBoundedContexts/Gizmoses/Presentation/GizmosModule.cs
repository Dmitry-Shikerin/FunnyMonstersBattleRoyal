using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Gizmoses.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Gizmoses.Presentation
{
    public class GizmosModule : EntityModule
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