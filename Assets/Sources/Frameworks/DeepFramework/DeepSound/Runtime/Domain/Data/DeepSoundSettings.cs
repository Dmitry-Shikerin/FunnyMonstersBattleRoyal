using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.Editor;
using Sources.Frameworks.DeepFramework.DeepUtils.Editor.Services;
using Sources.Frameworks.DeepFramework.DeepUtils.MyUiFramework.Utils;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data
{
    public class DeepSoundSettings : ScriptableObject
    {
        //Const
        private const string FileName = nameof(DeepSoundSettings);
        private const string ResourcesPath = "Assets/Resources/Services/Soundy/Settings/";
        private const string AssetPath = "Assets/Resources/Soundy/Settings/SoundySettings";
        private const string Asset = "t:SoundySettings";
        private const bool DefaultAutoKillIdleControllers = true;
        private const float DefaultControllerIdleKillDuration = 20f;
        private const float ControllerIdleKillDurationMin = 0f;
        private const float ControllerIdleKillDurationMax = 300f;
        private static readonly Vector2Int MinMaxControllersIdleKillDuration = 
            new ((int)ControllerIdleKillDurationMin, (int)ControllerIdleKillDurationMax);
        private const float DefaultIdleCheckInterval = 5f;
        private const float IdleCheckIntervalMin = 0.1f;
        private const float IdleCheckIntervalMax = 60f;
        private static readonly Vector2Int MixMaxIdleCheckInterval = 
            new ((int)IdleCheckIntervalMin, (int)IdleCheckIntervalMax);
        private const int DefaultMinimumNumberOfControllers = 3;
        private const int MinimumNumberOfControllersMin = 0;
        private const int MinimumNumberOfControllersMax = 20;
        private static readonly Vector2Int MixMaxMinimumNumberOfControllers = 
            new (MinimumNumberOfControllersMin, MinimumNumberOfControllersMax);
        private const string AutoKillIdleControllersGroup = "AutoKillIdleControllersGroup";
        private const string ControllerIdleKillDurationGroup = "ControllerIdleKillDuration";
        private const string IdleCheckIntervalGroup = "IdleCheckInterval";
        private const string MinimumNumberOfControllersGroup = "MinimumNumberOfControllers";

        public static DeepSoundSettings Instance
        {
            get
            {
                if (s_instance != null)
                    return s_instance;
#if UNITY_EDITOR
                s_instance = AssetDatabase.LoadAssetAtPath<DeepSoundSettings>(
                    "Services/DeepSound/DeepSoundSettings.asset");
#endif          

                if (s_instance != null)
                    return s_instance;
                
                s_instance = Resources.Load<DeepSoundSettings>(
                    "Services/DeepSound/DeepSoundSettings");
                
                if (s_instance != null)
                    return s_instance;

#if UNITY_EDITOR
                s_instance = CreateInstance<DeepSoundSettings>();
                AssetDatabase.CreateAsset(s_instance,
                    ResourcesPath + FileName + ".asset");
                AssetDatabase.SaveAssets();
#endif
                
                return s_instance;
            }
        }


        private static DeepSoundSettings s_instance;

        public static DeepSoundDataBase Database
        {
            get
            {
                if (Instance._database != null)
                    return Instance._database;
                
                UpdateDatabase();
                
                return Instance._database;
            }
        }

        public static void UpdateDatabase()
        {
            Instance._database = MyAssetUtils.GetScriptableObject<DeepSoundDataBase>(
                "_" + DeepSoundDataBase.FileName, "Assets/Resources/Soundy/DataBases");
#if UNITY_EDITOR
            if (Instance._database == null)
                return;
            
            Instance._database.Initialize();
            Instance.SetDirty(true);
#endif
        }

        [SerializeField] private DeepSoundDataBase _database;
        [HorizontalGroup(AutoKillIdleControllersGroup)]
        //[CustomValueDrawer(nameof(AutoKillIdleControllersValueDrawer))]
        [SerializeField] public bool AutoKillIdleControllers = DefaultAutoKillIdleControllers;
        [HorizontalGroup(ControllerIdleKillDurationGroup)]
        [PropertyRange(0, 300)]
        [SerializeField] public float ControllerIdleKillDuration = DefaultControllerIdleKillDuration;
        [HorizontalGroup(IdleCheckIntervalGroup)]
        [PropertyRange(0, 300)]
        [SerializeField] public float IdleCheckInterval = DefaultIdleCheckInterval;
        [HorizontalGroup(MinimumNumberOfControllersGroup)]
        [PropertyRange(MinimumNumberOfControllersMin, MinimumNumberOfControllersMax)]
        [SerializeField] public int MinimumNumberOfControllers = DefaultMinimumNumberOfControllers;

        private void Reset()
        {
            ResetAutoKillIdleControllers();
            ResetControllerIdleKillDuration();
            ResetIdleCheckInterval();
            ResetMinimumNumberOfControllers();
        }
        
        public void Reset(bool saveAssets)
        {
            Reset();
            SetDirty(saveAssets);
        }

        public void ResetComponent()
        {
        }
        
        [HorizontalGroup(ControllerIdleKillDurationGroup, 30)]
        [HideLabel]
        [Button(SdfIconType.ArrowClockwise)]
        public void ResetControllerIdleKillDuration() =>
            ControllerIdleKillDuration = DefaultControllerIdleKillDuration;
        
        [HorizontalGroup(AutoKillIdleControllersGroup, 30)]
        [HideLabel]
        [Button(SdfIconType.ArrowClockwise)]
        public void ResetAutoKillIdleControllers() =>
            AutoKillIdleControllers = DefaultAutoKillIdleControllers;
        
        [HorizontalGroup(IdleCheckIntervalGroup, 30)]
        [HideLabel]
        [Button(SdfIconType.ArrowClockwise)]
        public void ResetIdleCheckInterval() =>
            IdleCheckInterval = DefaultIdleCheckInterval;
        
        [HorizontalGroup(MinimumNumberOfControllersGroup, 30)]
        [HideLabel]
        [Button(SdfIconType.ArrowClockwise)]
        public void ResetMinimumNumberOfControllers() =>
            MinimumNumberOfControllers = DefaultMinimumNumberOfControllers;
        
        public void SetDirty(bool saveAssets) =>
            MyUtils.SetDirty(this, saveAssets);
        
        public void UndoRecord(string undoMessage) =>
            MyUtils.UndoRecordObject(this, undoMessage);
    }
}