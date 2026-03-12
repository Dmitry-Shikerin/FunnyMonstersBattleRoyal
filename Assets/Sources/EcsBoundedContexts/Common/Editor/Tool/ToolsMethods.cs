using Sources.EcsBoundedContexts.Common.Domain.Constants;
using UnityEditor;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Editor.Tool
{
    public class ToolsMethods
    {
        private const string Tools = "Tools/";
        
        private const string ClearPrefsItem = "Clear prefs";
        private const string ClearPrefsMenuItem = Tools + ClearPrefsItem;
        
        private const string GenerateItem = "Generate/";
        
        private const string GenerateComponentsItem = "Generate Components";
        private const string GenerateComponentsMenuItem = Tools + GenerateItem + GenerateComponentsItem;
        
        private const string GenerateSystemsItem = "Generate Systems";
        private const string GenerateSystemsMenuItem = Tools + GenerateItem + GenerateSystemsItem;
        
        [MenuItem(ClearPrefsMenuItem)]
        public static void ClearPrefs()
        {
            foreach (string id in IdsConst.GetAll())
            {
                //Debug.Log($"Deleted {id}");
                PlayerPrefs.DeleteKey(id);
            }

            PlayerPrefs.Save();
        }
    }
}