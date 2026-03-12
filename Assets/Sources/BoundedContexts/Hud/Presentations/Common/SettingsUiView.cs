using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Common
{
    public class SettingsUiView : UiView
    {
        [field: Required] [field: SerializeField] public EntityLink SoundVolumeLink { get; private set; }        
        [field: Required] [field: SerializeField] public EntityLink MusicVolumeLink { get; private set; }
    }
}