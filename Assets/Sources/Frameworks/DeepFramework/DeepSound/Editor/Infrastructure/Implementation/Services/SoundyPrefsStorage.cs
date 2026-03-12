using UnityEngine;
using UnityEditor;

namespace Sources.Frameworks.DeepFramework.DeepSound.Editor.Infrastructure.Implementation.Services
{
    public static class SoundyPrefsStorage
    {
        public const string SoundySettings = "SoundySettings";
        
        private const string LastSoundGroupDataKey = "LastSoundGroupData";
        private const string LastDataTabKey = "LastDataTab";
        
        public static void SaveLastSoundGroupData(string name) => 
            PlayerPrefs.SetString(LastSoundGroupDataKey, name);
        
        public static string GetLastSoundGroupData() => 
            PlayerPrefs.GetString(LastSoundGroupDataKey);

        public static string GetLastDataTab() =>
            PlayerPrefs.GetString(LastDataTabKey);
        
        public static void SaveLastDataTab(string name) =>
            PlayerPrefs.SetString(LastDataTabKey, name);
    }
}