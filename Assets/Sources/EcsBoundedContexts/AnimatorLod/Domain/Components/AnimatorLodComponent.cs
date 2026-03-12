using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimatorLod.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.AnimatorLod)]
    [Aspect(AspectName.Game)]
    public struct AnimatorLodComponent
    {
        [Tooltip("If true, changes the skin weights of the skinned mesh renderers based on LOD")]
        public bool ChangeSkinWeights;
        [Tooltip("The skinned mesh renderers to disable/enable")]
        public SkinnedMeshRenderer[] SkinnedMeshRenderers;
        public int FrameCountdown;
    }
}