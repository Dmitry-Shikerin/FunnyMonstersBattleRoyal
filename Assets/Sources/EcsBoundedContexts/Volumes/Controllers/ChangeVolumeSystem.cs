using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Volumes.Domain.Components;
using Sources.EcsBoundedContexts.Volumes.Domain.Enums;
using Sources.EcsBoundedContexts.Volumes.Presentation;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Volumes.Controllers
{
    [EcsSystem(83)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public class ChangeVolumeSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _increaseIt = new(
            It.Inc<
                GameVolumeComponent,
                VolumeTypeComponent,
                IncreaseEvent>());
        [DI] private readonly ProtoIt _decreaseIt = new(
            It.Inc<
                GameVolumeComponent,
                VolumeTypeComponent,
                DecreaseEvent>());
        [DI] private readonly ProtoIt _muteIt = new(
            It.Inc<
                GameVolumeComponent,
                VolumeTypeComponent,
                MuteVolumeEvent>());
        [DI] private readonly ProtoIt _unmuteIt = new(
            It.Inc<
                GameVolumeComponent,
                VolumeTypeComponent,
                UnmuteVolumeEvent>());        
        [DI] private readonly ProtoIt _changeIt = new(
            It.Inc<
                GameVolumeComponent,
                VolumeTypeComponent,
                ChangeVolumeEvent>());

        private readonly ISoundService _soundService;
        private readonly IEntityRepository _entityRepository;

        public ChangeVolumeSystem(
            ISoundService soundService,
            IEntityRepository entityRepository)
        {
            _soundService = soundService;
            _entityRepository = entityRepository;
        }

        public void Init(IProtoSystems systems)
        {
            //UpdateModule(IdsConst.SoundsVolume);
            //UpdateModule(IdsConst.MusicVolume);
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _changeIt)
            {
                float eventValue = entity.GetChangeVolumeEvent().Value;
                float nextVolume = Math.Clamp(eventValue, 0, 1);
                entity.ReplaceGameVolume(nextVolume);
                VolumeType volumeType = entity.GetVolumeType().Value;

                if (volumeType == VolumeType.Music)
                {
                    UpdateModule(IdsConst.MusicVolume);
                    _soundService.ChangeMusicVolume(nextVolume);
                }
                else if (volumeType == VolumeType.Sounds)
                {
                    UpdateModule(IdsConst.SoundsVolume);
                    _soundService.ChangeSoundsVolume(nextVolume);
                }
            }
            
            foreach (ProtoEntity entity in _increaseIt)
            {
                float currentVolume = entity.GetGameVolume().Value;
                float nextVolume = Math.Clamp(currentVolume + 0.1f, 0, 1);
                entity.ReplaceGameVolume(nextVolume);
                VolumeType volumeType = entity.GetVolumeType().Value;

                if (volumeType == VolumeType.Music)
                {
                    _soundService.ChangeMusicVolume(nextVolume);
                }
                else if (volumeType == VolumeType.Sounds)
                {
                    _soundService.ChangeSoundsVolume(nextVolume);
                }
            }

            foreach (ProtoEntity entity in _decreaseIt)
            {
                float currentVolume = entity.GetGameVolume().Value;
                float nextVolume = Math.Clamp(currentVolume - 0.1f, 0, 1);
                entity.ReplaceGameVolume(nextVolume);
                VolumeType volumeType = entity.GetVolumeType().Value;

                if (volumeType == VolumeType.Music)
                    _soundService.ChangeMusicVolume(nextVolume);
                else if (volumeType == VolumeType.Sounds)
                    _soundService.ChangeSoundsVolume(nextVolume);
            }

            foreach (ProtoEntity entity in _muteIt)
            {
                entity.AddMutedVolume();
                VolumeType volumeType = entity.GetVolumeType().Value;

                if (volumeType == VolumeType.Music)
                    _soundService.MuteMusic();
                else if (volumeType == VolumeType.Sounds)
                    _soundService.MuteSounds();
            }

            foreach (ProtoEntity entity in _unmuteIt)
            {
                entity.DelMutedVolume();
                VolumeType volumeType = entity.GetVolumeType().Value;

                if (volumeType == VolumeType.Music)
                    _soundService.UnmuteMusic();
                else if (volumeType == VolumeType.Sounds)
                    _soundService.UnmuteSounds();
            }
        }

        private void UpdateModule(string id)
        {
            ProtoEntity entity = _entityRepository.GetByName(id);
            VolumeModule module = entity.GetVolumeModule().Value;
            float volume = entity.GetGameVolume().Value;
            EnableState state = entity.HasMutedVolume() ? EnableState.Off : EnableState.On;
            module.MuteToggle.SetState(state);
            module.UiStepper.SetValue(volume);
        }
    }
}