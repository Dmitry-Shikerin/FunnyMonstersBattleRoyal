using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Dictionaries;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.MyUiFramework.Utils;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = nameof(DeepSoundDataBase), menuName = "Configs/DeepFramework/" + nameof(DeepSoundDataBase), order = 51)]
    public class DeepSoundDataBase : ScriptableObject
    {
        //Const
        private const string RefreshDatabasePhrase = "Refresh Database";
        private const string RenameSoundDatabasePhrase = "Rename Sound Database";
        private const string EnterDatabaseName = "Enter a database name";
        private const string Ok = "Ok";
        private const string No = "No";
        private const string Yes = "Yes";
        private const string NewSoundDatabase = "New Sound Database";
        private const string AnotherEntryExists = "There is another entry with the same name.";
        private const string DeleteDatabasePhrase = "Delete Database";
        private const string AreYouSureYouWantToDeleteDatabase = "Are you sure you want tto delete database";
        private const string OperationCannotBeUndone = "Operation Cannot Be Undone";
        public const string FileName = "SoundyDatabase";
        
        [TabGroup("Data")]
        [SerializeField] private SoundDataBaseDictionary _dataBases = new ();
        [TabGroup("Create")]
        [SerializeField] private SoundDatabaseName _name;
        
        public IEnumerable<SoundDatabaseName> GetDatabaseNames() =>
            _dataBases.Keys.ToList();
        
        public IEnumerable<SoundDataBase> GetSoundDatabases() =>
            _dataBases.Values.ToList();
        
        public bool AddSoundDatabase(SoundDataBase database)
        {
            if (database == null)
                return false;
            
            _dataBases ??= new SoundDataBaseDictionary();
            _dataBases[database.Name] = database;
            
            return true;
        }

#if UNITY_EDITOR
        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
        
        [TabGroup("Create")]
        [PropertySpace(10)] 
        [Button]
        private void CreateConfig()
        {
            if (_name == SoundDatabaseName.Default)
                return;
            
            if (_dataBases.Values.Any(soundDataBase => soundDataBase.Name == _name))
            {
                EditorDialogUtils.ShowErrorDialog(
                    $"{nameof(DeepSoundDataBase)}",
                    $"This id not unique: {_name}");

                return;
            }

            SoundDataBase dataBase = CreateInstance<SoundDataBase>();
            dataBase.Parent = this;
            AssetDatabase.AddObjectToAsset(dataBase, this);
            dataBase.Name = _name;
            dataBase.name = $"{_name}_{nameof(SoundDataBase)}";
            _dataBases.Add(_name, dataBase);
            dataBase.Initialize();
            AddSoundDatabase(dataBase);
            AssetDatabase.SaveAssets();
        }
#endif
        
        public bool Contains(SoundDatabaseName databaseName) =>
            _dataBases.ContainsKey(databaseName);

        public bool Contains(SoundDatabaseName databaseName, SoundName soundName) =>
            Contains(databaseName) && GetSoundDatabase(databaseName).DataBase.ContainsKey(soundName);
        
        public bool DeleteDatabase(SoundDataBase database)
        {
            if (database == null)
                return false;

            if (_dataBases.ContainsValue(database) == false)
                return false;
            
            _dataBases.Remove(database.Name);
            return true;
        }    
        
        public SoundGroupData GetAudioData(SoundDatabaseName databaseName, SoundName soundName) => 
            Contains(databaseName) == false ? null : GetSoundDatabase(databaseName).GetData(soundName);

        public SoundGroupData GetSoundGroupData(SoundName soundName)
        {
            foreach (SoundDataBase dataBase in _dataBases.Values)
            {
                foreach (SoundGroupData soundGroupData in dataBase.GetSoundDatabases())
                {
                    if (soundGroupData.SoundName != soundName)
                        continue;
                    
                    return soundGroupData;
                }
            }

            throw new NullReferenceException();
        }
        
        public SoundDataBase GetSoundDatabase(SoundDatabaseName databaseName)
        {
            if (_dataBases == null)
            {
                _dataBases = new SoundDataBaseDictionary();
                return null;
            }

            return _dataBases.GetValueOrDefault(databaseName);
        }
        
        public void Initialize()
        {
        }
        
        public void InitializeSoundDatabases()
        {
            if (_dataBases == null)
                return;

            foreach (SoundDataBase dataBase in _dataBases.Values)
                dataBase.Initialize();

            //after removing any null references the database is still empty -> initialize it and add the 'General' sound database
            if (_dataBases.Count == 0)
                Initialize();
        }
        
        public void RefreshDatabase()
        {
            Initialize();
            
            foreach (SoundDataBase soundDatabase in _dataBases.Values)
                soundDatabase.RefreshDatabase();
        }
        
        public bool RenameSoundDatabase(SoundDataBase soundDatabase, SoundDatabaseName newDatabaseName)
        {
            if (soundDatabase == null)
                return false;

#if UNITY_EDITOR
            if (Contains(newDatabaseName))
            {
                EditorUtility.DisplayDialog(
                    RenameSoundDatabasePhrase + " '" + soundDatabase.Name + "'",
                    NewSoundDatabase + ": '" + newDatabaseName + "" + "\n\n" + 
                    AnotherEntryExists, 
                    Ok);

                return false;
            }

            _dataBases.Remove(soundDatabase.Name);
            soundDatabase.Name = newDatabaseName;
            _dataBases[soundDatabase.Name] = soundDatabase;
            Save();
#endif
            return true;
        }
    }
}