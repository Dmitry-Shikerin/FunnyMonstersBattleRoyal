using System;
using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data
{
    public interface IDataService
    {
        object LoadData(string key, Type type);
        T LoadData<T>(string key)
            where T : IEntitySaveData;
        void SaveData<T>(T dataModel, string key)
            where T : IEntitySaveData;
        bool HasKey(string key);
        void Clear(string key);
    }
}