using System;
using System.Collections.Generic;
using Sources.EcsBoundedContexts.Gizmoses.Presentation;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Gizmoses.Domain
{
    [Serializable]
    public struct DrawerContainer
    {
        [SerializeReference] [TypeFilter(nameof(Get))]
        public DebugGizmosDrawer Drawer;

        private IEnumerable<Type> Get() =>
            ReflectionUtils.GetFilteredTypeList<DebugGizmosDrawer>();
    }
}