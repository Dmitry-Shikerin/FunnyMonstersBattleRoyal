using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Common;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Settings.Domain.Data;
using Sources.EcsBoundedContexts.Settings.Infrastructure;
using Sources.EcsBoundedContexts.Settings.Presentation.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Controllers.Data
{
    [EcsSystem(10)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.MainMenu, AspectName.Game, AspectName.Lobby)]
    public class SettingsLoadSystem : IProtoInitSystem
    {
        private readonly ISceneService _sceneService;
        private readonly IUiViewService _uiViewService;
        private readonly SettingsEntityFactory _factory;
        private readonly IDataService _dataService;

        public SettingsLoadSystem(
            ISceneService sceneService,
            IUiViewService uiViewService,
            SettingsEntityFactory factory,
            IDataService dataService)
        {
            _sceneService = sceneService;
            _uiViewService = uiViewService;
            _factory = factory;
            _dataService = dataService;
        }

        public void Init(IProtoSystems systems)
        {
            EntityLink link = _uiViewService.Get<SettingsUiView>().SettingsLink;
            ProtoEntity settingsEntity = _factory.Create(link);

            if (_dataService.HasKey(IdsConst.Settings) == false)
            {
                UpdateModules(link);
                return;
            }
            
            //Load
            SettingsSaveData settingsSaveData = _dataService.LoadData<SettingsSaveData>(IdsConst.Settings);
            settingsEntity.ReplaceSavedSettings(settingsSaveData);
            
            //Music
            settingsEntity.ReplaceMusicVolume(settingsSaveData.MusicVolume);

            if (settingsSaveData.IsMusicMuted)
                settingsEntity.AddMutedMusicVolume();
            
            //Volume
            settingsEntity.ReplaceSoundVolume(settingsSaveData.SoundVolume);

            if (settingsSaveData.IsSoundMuted)
                settingsEntity.AddMutedSoundVolume();
            
            //FullScreen
            settingsEntity.ReplaceFullScreenMode(settingsSaveData.FullScreenMode);
            
            //Framerate
            settingsEntity.ReplaceFramerate(settingsSaveData.Framerate);
            Debug.Log($"Framerate {settingsSaveData.Framerate}");
            
            //VSync
            if (settingsSaveData.IsVSync)
                settingsEntity.AddVSync();
            
            //Resolution
            ResolutionSaveData resolutionData = settingsSaveData.Resolution;
            settingsEntity.ReplaceResolution(resolutionData.Width, resolutionData.Height, resolutionData.RefreshRate);
            
            //Finish
            UpdateModules(link);
            settingsEntity.AddInitialized();
        }

        private void UpdateModules(EntityLink link)
        {
            IReadOnlyList<EntityModule> modules = link.GetModules();

            foreach (EntityModule module in modules)
            {
                if (module is not ISettingsModule concrete)
                    continue;
                    
                concrete.UpdateView();
            }
        }
    }
}