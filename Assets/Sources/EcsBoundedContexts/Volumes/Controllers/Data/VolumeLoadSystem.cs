using System;
using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Common;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Volumes.Domain.Data;
using Sources.EcsBoundedContexts.Volumes.Infrastructure;
using Sources.EcsBoundedContexts.Volumes.Presentation;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Volumes.Controllers.Data
{
    [EcsSystem(7)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public class VolumeLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly ISoundService _soundService;
        private readonly IDataService _dataService;
        private readonly IEntityRepository _entityRepository;
        private readonly VolumeEntityFactory _volumeEntityFactory;

        public VolumeLoadSystem(
            IUiViewService uiViewService,
            ISoundService soundService,
            IDataService dataService,
            IEntityRepository entityRepository,
            VolumeEntityFactory volumeEntityFactory)
        {
            _uiViewService = uiViewService;
            _soundService = soundService;
            _dataService = dataService;
            _entityRepository = entityRepository;
            _volumeEntityFactory = volumeEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            SettingsUiView settingsUiView = _uiViewService.Get<SettingsUiView>();

            //Create
            ProtoEntity soundEntity = _volumeEntityFactory.Create(settingsUiView.SoundVolumeLink, IdsConst.SoundsVolume);
            ProtoEntity musicEntity = _volumeEntityFactory.Create(settingsUiView.MusicVolumeLink, IdsConst.MusicVolume);

            if (_dataService.HasKey(IdsConst.MusicVolume) == false)
            {
                settingsUiView.SoundVolumeLink.GetModule<VolumeModule>().UiStepper.SetValue(0.6f);
                soundEntity.AddChangeVolumeEvent(0.6f);
                soundEntity.AddUnmuteVolumeEvent();
                settingsUiView.MusicVolumeLink.GetModule<VolumeModule>().UiStepper.SetValue(0.5f);
                musicEntity.AddUnmuteVolumeEvent();
                musicEntity.AddChangeVolumeEvent(0.6f);
                
                return;
            }

            //Load
            Load(IdsConst.MusicVolume, _soundService.ChangeMusicVolume, _soundService.MuteMusic);
            Load(IdsConst.SoundsVolume, _soundService.ChangeSoundsVolume, _soundService.MuteSounds);
        }

        private void Load(string id, Action<float> changeVolume, Action mute)
        {
            GameVolumeSaveData gameVolumeData = _dataService.LoadData<GameVolumeSaveData>(id);
            ProtoEntity entity = _entityRepository.GetByName(id);
            entity.ReplaceGameVolume(gameVolumeData.Value);
            changeVolume.Invoke(gameVolumeData.Value);

            if (gameVolumeData.IsMuted)
            {
                entity.AddMutedVolume();
                mute.Invoke();
            }
        }
    }
}