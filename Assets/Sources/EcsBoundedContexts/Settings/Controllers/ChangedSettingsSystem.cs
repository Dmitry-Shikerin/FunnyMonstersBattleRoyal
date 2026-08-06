using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Settings.Domain.Components;
using Sources.EcsBoundedContexts.Settings.Presentation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;

namespace Sources.EcsBoundedContexts.Settings.Controllers
{
    [EcsSystem(50)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.MainMenu, AspectName.Game, AspectName.Lobby)]
    public class ChangedSettingsSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                SettingsTag,
                ChangedSettingsComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                SetterSettingsModule module = entity.GetSetterSettingsModule().Value;
                UiButton button = module.ApplyButton;

                if (button.gameObject.activeSelf)
                    continue;
                
                button.gameObject.SetActive(true);
            }
        }
    }
}