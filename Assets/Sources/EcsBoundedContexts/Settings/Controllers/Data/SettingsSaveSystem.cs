using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.Settings.Domain.Components;
using Sources.EcsBoundedContexts.Settings.Domain.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.Settings.Controllers.Data
{
    [EcsSystem(504)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.MainMenu, AspectName.Game, AspectName.Lobby)]
    public class SettingsSaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                SettingsTag,
                SaveDataEvent>());
        [DI] private readonly ProtoIt _clearIt = new(
            It.Inc<
                SettingsTag,
                ClearDataEvent>());

        public SettingsSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                SettingsSaveData data = new SettingsSaveData
                {
                    Id = IdsConst.Settings,
                    MusicVolume = entity.GetMusicVolume().Value,
                    IsMusicMuted = entity.HasMutedMusicVolume(),
                    SoundVolume = entity.GetSoundVolume().Value,
                    IsSoundMuted = entity.HasMutedSoundVolume(),
                };
                
                _dataService.SaveData(data, IdsConst.Settings);
                entity.ReplaceSavedSettings(data);
            }

            foreach (ProtoEntity entity in _clearIt)
            {
                _dataService.Clear(IdsConst.Settings);
            }
        }
    }
}