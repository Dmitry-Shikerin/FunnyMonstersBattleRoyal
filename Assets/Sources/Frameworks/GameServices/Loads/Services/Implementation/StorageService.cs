using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Loads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.Frameworks.GameServices.Loads.Services.Implementation
{
    public class StorageService : IStorageService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IDataService _dataService;

        public StorageService(
            IEntityRepository entityRepository,
            IDataService dataService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }
        
        public T Load<T>(string id) 
            where T : struct, IEntitySaveData
        {
            object entity = _dataService.LoadData(id, IdsConst.AllIds[id].Type);

            if (entity == null)
                throw new NullReferenceException(id);
            
            if (entity is not T concrete)
                throw new InvalidCastException(nameof(T));

            if (concrete.Id == null)
                throw new NullReferenceException(typeof(T).Name);

            return concrete;
        }

        public void Save(IEntitySaveData entity)
        {
            _dataService.SaveData(entity, entity.Id);
        }

        public void Save(string id)
        {
            ProtoEntity entity = _entityRepository.GetByName(id);
            
            if (entity.HasSaveDataEvent())
                return;
            
            entity.AddSaveDataEvent();
        }

        public void SaveAll()
        {
            foreach (string id in IdsConst.GetAll())
            {
                ProtoEntity entity = _entityRepository.GetByName(id);

                if (entity.HasSaveDataEvent())
                    continue;
                
                entity.AddSaveDataEvent();
            }
        }

        public void Save(IEnumerable<string> ids)
        {
            foreach (string id in ids)
            {
                ProtoEntity entity = _entityRepository.GetByName(id);
                entity.AddSaveDataEvent();
            }
        }

        public void Clear(IEntitySaveData entity) =>
            _dataService.Clear(entity.Id);

        public void Clear(string id) =>
            _dataService.Clear(id);

        public void ClearAll()
        {
            foreach (string id in IdsConst.GetDeleteIds())
            {
                // if (_entityRepository.TryGetByName(id, out ProtoEntity entity) == false)
                //     continue;
                //
                // entity.AddClearDataEvent();
                _dataService.Clear(id);
            }
        }

        public void Clear(IEnumerable<string> ids)
        {
            foreach (string id in ids)
                _dataService.Clear(id);
        }

        public bool HasKey(string id) =>
            _dataService.HasKey(id);
    }
}