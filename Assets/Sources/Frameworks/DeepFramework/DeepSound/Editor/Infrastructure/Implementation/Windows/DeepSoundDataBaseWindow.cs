#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sources.Frameworks.DeepFramework.DeepSound.Editor.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepSound.Editor.Infrastructure.Implementation.Services;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepUtils.Editor;
using Sources.Frameworks.DeepFramework.DeepUtils.Editor.Services;
using UnityEditor;

namespace Sources.Frameworks.DeepFramework.DeepSound.Editor.Infrastructure.Implementation.Windows
{
    public class DeepSoundDataBaseWindow : OdinMenuEditorWindow
    {
        [MenuItem(SoundConst.MenuItem)]
        private static void OpenWindow()
        {
            GetWindow<DeepSoundDataBaseWindow>().Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EditorUpdateService.Initialize();
            PreviewSoundPlayerService.Initialize();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PreviewSoundPlayerService.Destroy();
            EditorUpdateService.Destroy();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            tree.Add("Settings", DeepSoundSettings.Instance);
            //tree.Add("Configure", new DSConfigureView(this));
            
            IEnumerable<SoundDataBase> databases = DeepSoundSettings.Database.GetSoundDatabases();

            foreach (SoundDataBase soundDatabase in databases)
                tree.Add(soundDatabase.Name.ToString(), soundDatabase);

            return tree;
        }
    }
}
#endif
