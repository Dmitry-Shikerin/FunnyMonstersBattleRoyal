using System.Collections.Generic;
using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.Frameworks.GameServices.Loads.Services.Interfaces
{
    public interface IStorageService
    {
        T Load<T>(string id) 
            where T : struct, IEntitySaveData;
        void Save(string id);
        void SaveAll();
        void Save(IEnumerable<string> ids);
        void Clear(string id);
        void ClearAll();
        void Clear(IEnumerable<string> ids);
        bool HasKey(string id);
    }
}