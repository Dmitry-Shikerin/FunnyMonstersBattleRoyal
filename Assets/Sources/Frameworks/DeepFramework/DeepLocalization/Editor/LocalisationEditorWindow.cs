#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using Sources.Frameworks.DeepFramework.DeepLocalization.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data;
using UnityEditor;

namespace Sources.Frameworks.DeepFramework.DeepLocalization.Editor
{
#if UNITY_EDITOR
    public class LocalisationEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem(LocalizationConst.LocalizationMenuItem)]
        private static void OpenWindow()
        {
            GetWindow(typeof(LocalisationEditorWindow)).Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            tree.Add("Database", LocalizationDataBase.Instance);
            
            foreach (LocalizationPhrase phrase in LocalizationDataBase.Instance.Phrases)
                tree.Add($"Phrases/{phrase.LocalizationId}", phrase);
            
            return tree;
        }
    }
#endif
}