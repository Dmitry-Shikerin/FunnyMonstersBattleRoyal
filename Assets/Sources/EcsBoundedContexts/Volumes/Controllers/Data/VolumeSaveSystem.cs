using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.Volumes.Domain.Components;
using Sources.EcsBoundedContexts.Volumes.Domain.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.Volumes.Controllers.Data
{
    [EcsSystem(501)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public class VolumeSaveSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                GameVolumeComponent,
                SaveDataEvent>());

        private readonly IDataService _dataService;

        public VolumeSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                float volume = entity.GetGameVolume().Value;
                string id = entity.GetStringId().Value;

                GameVolumeSaveData data = new GameVolumeSaveData
                {
                    Id = id,
                    Value = volume,
                    IsMuted = entity.HasMutedVolume(),
                };
                _dataService.SaveData(data, id);
            }
        }
    }
}