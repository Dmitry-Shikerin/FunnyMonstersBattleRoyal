using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Dictionaries;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data
{
    public class SoundDataBase : ScriptableObject
    {
        //Const
        public const string RefreshDatabaseConst = "Refresh Database";
        public const string GeneralName = "SoundDataBase_Genera";
        public const string RemovedEntryConst = "Removed Entry";
        public const string RemovedDuplicateEntriesConst = "Removed duplicate entries";
        public const string RemoveEmptyEntriesConst = "Remove Empty Entries";        
        public const string SortDatabaseConst = "Sort Database";
        public const string AddItemConst = "Add Items";
        public const string ResourcesPath = "Assets/Resources/Soundy/DataBases";
        
        [DisplayAsString(false)] 
        [HideLabel] 
        [SerializeField] private string _label = DeepSoundConst.SoundDataBaseLabel;
        [SerializeField] public SoundDatabaseName Name;
        [SerializeField] public AudioMixerGroup OutputAudioMixerGroup;
        [PropertySpace(20)]
        [SerializeField] private SoundGroupDataDictionary _dataBase = new ();
        [HideInInspector]
        [SerializeField] public DeepSoundDataBase Parent;
        
        public SoundGroupDataDictionary DataBase => _dataBase;

        public bool HasSoundsWithMissingAudioClips => _dataBase.Values.Any(data => data.HasMissingAudioClips);
        
        public IEnumerable<SoundName> GetSoundNames() =>
            _dataBase.Keys;
        
        public IEnumerable<SoundGroupData> GetSoundDatabases() =>
            _dataBase.Values;
        
        public bool Add(SoundGroupData data)
        {
            if (data == null)
                return false;

            data.ParentDatabaseName = Name;

            return true;
        }      
        
        public bool Add(SoundName soundName, SoundGroupData data)
        {
            if (data == null)
                return false;

            data.ParentDatabaseName = Name;
            
            // soundName = soundName.Trim();
            // string newName = soundName;
            // int counter = 0;
            //
            // while (Contains(newName))
            // {
            //     counter++;
            //     newName = soundName + " (" + counter + ")";
            // }
            //
            // data.SoundName = newName;
            _dataBase[soundName] = data;

            return true;
        }
        
        public SoundGroupData Add(SoundName soundName)
        {
            // soundName = soundName.Trim();
            // string newName = soundName;
            // int counter = 0;

            // while (Contains(newName))
            // {
            //     counter++;
            //     newName = soundName + " (" + counter + ")";
            // }
            
            SoundGroupData data = new SoundGroupData();
            data.ParentDatabaseName = Name;
            data.SoundName = soundName;

            _dataBase ??= new SoundGroupDataDictionary();
            _dataBase[soundName] = data;

            return data;
        }
        
        public bool Contains(SoundName soundName) =>
            _dataBase.ContainsKey(soundName);

        public bool Contains(SoundGroupData soundGroupData) =>
            soundGroupData != null && _dataBase.ContainsValue(soundGroupData);

        public SoundGroupData GetData(SoundName soundName) =>
            _dataBase.GetValueOrDefault(soundName);

        public void Initialize() =>
            RefreshDatabase();
        
        public bool Remove(SoundGroupData data)
        {
            if (data == null)
                return false;

            if (Contains(data) == false)
                return false;

            _dataBase.Remove(data.SoundName);
            AddNoSound();

            return true;
        }
        
        public void RefreshDatabase()
        {
            AddNoSound();
            // RemoveUnnamedEntries();
            // RemoveDuplicateEntries();
            CheckAllDataForCorrectDatabaseName();
        }
        
        //TODO сделать конвертацию для этих методов
        public void RemoveDuplicateEntries() =>
            _dataBase = (SoundGroupDataDictionary)_dataBase.Values
                .GroupBy(data => data.SoundName)
                .Select(data => data.First())
                .ToDictionary(data => data.SoundName, data => data);

        // public void RemoveUnnamedEntries() =>
        //     _dataBase = (SoundGroupDataDictionary)_dataBase.Values
        //         .Where(data => string.IsNullOrEmpty(data.SoundName.Trim()) == false)
        //         .ToDictionary(data => data.SoundName, data => data);
        
        private bool AddNoSound()
        {
            if (Contains(SoundName.Default))
                return false;
            
            if (_dataBase == null)
                _dataBase = new SoundGroupDataDictionary();

            SoundGroupData data = new SoundGroupData();
            _dataBase[SoundName.Default] = data;
            data.ParentDatabaseName = Name;
            data.SoundName = SoundName.Default;
            
            return true;
        }
        
        private bool CheckAllDataForCorrectDatabaseName()
        {
            bool foundSoundGroupWithWrongDatabaseName = false;

            foreach (SoundGroupData data in _dataBase.Values)
            {
                if (data == null)
                    continue;

                if (data.ParentDatabaseName.Equals(Name))
                    continue;

                foundSoundGroupWithWrongDatabaseName = true;
                data.ParentDatabaseName = Name;
            }

            return foundSoundGroupWithWrongDatabaseName;
        }
    }
}