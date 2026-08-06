using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Presentation
{
    public class VSyncSettingsModule : EntityModule
    {
        [Required] [SerializeField] private UiToggle _toggle;
    }
}