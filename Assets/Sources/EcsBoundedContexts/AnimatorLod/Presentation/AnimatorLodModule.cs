using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimatorLod.Presentation
{
    public class AnimatorLodModule : EntityModule
    {
        [field: Tooltip("If true, changes the skin weights of the skinned mesh renderers based on LOD")]
        [field: SerializeField] public bool ChangeSkinWeights { get; private set; } = true;
        [field: Tooltip("The animator component of this object")]
        [field: SerializeField] public Animator TrackedAnimatorComponent { get; private set; }
        [field: Tooltip("The transform to track")]
        [field: SerializeField] public Transform TrackedTransform { get; private set; }
        [field: Tooltip("The skinned mesh renderers to disable/enable")]
        [field: SerializeField] public List<SkinnedMeshRenderer> SkinnedMeshRenderers;
        [field: SerializeField] public int FrameCountdown { get; private set; }
    }
}