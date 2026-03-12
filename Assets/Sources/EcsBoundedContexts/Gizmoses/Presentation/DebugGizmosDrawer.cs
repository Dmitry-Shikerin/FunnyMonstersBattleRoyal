using System;
using Sources.EcsBoundedContexts.Gizmoses.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Gizmoses.Presentation
{
    [Serializable]
    public class DebugGizmosDrawer
    {
        [field: SerializeField] public GizmosDrawType DrawType { get; private set; } = GizmosDrawType.Selected;
        
        public virtual void Draw(GameObject obj)
        {
        }
    }
}