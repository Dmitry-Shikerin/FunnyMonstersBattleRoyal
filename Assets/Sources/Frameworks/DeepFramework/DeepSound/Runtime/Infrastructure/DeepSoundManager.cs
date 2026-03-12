using System;
using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepCores.Presentation;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure.Factories;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using Sources.Frameworks.DeepFramework.DeepUtils.MyUiFramework.Utils;
using Sources.Frameworks.DeepFramework.DeepUtils.Singletones;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure
{
    [AddComponentMenu(ComponentMenuMenuName, MenuOrder)]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-8)]
    public class DeepSoundManager : MonoBehaviourSingleton<DeepSoundManager>, IDeepCoreChild
    {
        //Const
        private const int MenuOrder = 13;
        private const string ComponentMenuPath = "DeepFramework/DeepSound/";
        private const string MenuItemPath = "GameObject/DeepFramework/DeepSound/";
        private const string GameObjectName = "Deep Sound Manager";
        private const string ComponentMenuMenuName = ComponentMenuPath + GameObjectName;
        private const string MenuItemName = MenuItemPath + GameObjectName;
        private const int MenuItemPriority = MenuOrder;
        public const string General = "General";
        public const string DatabasePhrase = "Database";
        public const string NewSoundGroup = "New Sound Group";
        public const string NoSound = "No Sound";
        public const string Sounds = "Sounds";
        
#if UNITY_EDITOR
        [MenuItem(MenuItemName, false, MenuItemPriority)]
        private static void CreateComponent(MenuCommand menuCommand)
        {
            DeepSoundManager addToScene = MyUtils.AddToScene<DeepSoundManager>(
                GameObjectName, true, true);
        }
#endif
        
        private readonly List<DeepSoundController> _cachedControllers = new();
        private readonly List<DeepSoundSender> _senders = new ();
        private readonly List<DeepSoundSender> _cachedSenders = new();
        
        private DeepSoundControllersPool _pool;
        private SoundControllerFactory _factory;
        private DeepCore _core;

        public static bool IsMuteAllControllers { get; private set; }
        public static bool IsPauseAllControllers { get; private set; }
        public static DeepSoundDataBase Database => DeepSoundSettings.Database;
        public static DeepSoundSettings Settings => DeepSoundSettings.Instance;
        public DeepSoundControllersPool Pool => _pool;
        public List<DeepSoundSender> Senders => _senders;
        public GameObject GameObject => gameObject;

        private void Awake()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            _pool = new DeepSoundControllersPool(Settings, this);
            _factory = new SoundControllerFactory(this, _pool);
            Initialize();
            _pool.Initialize(_factory);
        }

        private void Initialize()
        {
            DeepSoundManager[] soundManagers = FindObjectsOfType<DeepSoundManager>();

            foreach (DeepSoundManager manager in soundManagers)
            {
                if (manager == this)
                    continue;
                
                Destroy(manager.gameObject);
            }
            
            DeepSoundController[] soundControllers = FindObjectsOfType<DeepSoundController>();

            foreach (DeepSoundController controller in soundControllers)
                Destroy(controller.gameObject);
            
            _core = DeepCore.Instance;
            _core.AddChild(this);
        }

        public void Destroy()
        {
            Pool?.Destroy();
            Destroy(gameObject);
        }

        public static void Register(DeepSoundSender sender) =>
            Instance._senders.Add(sender);

        public static void Unregister(DeepSoundSender sender) =>
            Instance?._senders.Remove(sender);

        public static DeepSoundController Play(SoundDatabaseName databaseName, SoundName soundName)
        {
            SoundGroupData data = GetData(out SoundDataBase dataBase, databaseName, soundName);
            data.ChangeLastPlayedAudioData();

            return Instance.Pool.Get()
                .SetAudioClip(data.LastPlayedAudioData.AudioClip)
                .SetVolume(data.RandomVolume)
                .SetPitch(data.RandomPitch)
                .SetLoop(data.Loop)
                .SetSpatialBlend(data.SpatialBlend)
                .SetOutputAudioMixerGroup(dataBase.OutputAudioMixerGroup)
                .SetPosition(Instance.transform.position)
                .SetName(databaseName, data.SoundName, data.LastPlayedAudioData.AudioClip.name)
                .Play();
        }

        private static SoundGroupData GetData(out SoundDataBase database, SoundDatabaseName databaseName, SoundName soundName)
        {
            database = Database.GetSoundDatabase(databaseName);

            if (database == null)
                throw new NullReferenceException();

            return database.GetData(soundName) ?? throw new NullReferenceException();
        }

        public static DeepSoundController Play(AudioClip audioClip)
        {
            if (audioClip == null)
                throw new NullReferenceException(nameof(AudioClip));

            return Instance.Pool.Get()
                .SetAudioClip(audioClip)
                .SetName(SoundDatabaseName.Default, SoundName.Default, audioClip.name)
                .SetPosition(Instance.transform.position)
                .Play();
        }

        public static void Stop(SoundName soundName)
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
            {
                if (controller.Name != soundName)
                    continue;
                
                controller.Stop();
            }
        }

        public static void Mute(SoundDatabaseName databaseName)
        {
            ApplyWhere(databaseName, controller => controller.Mute());
            ApplyWhere(databaseName, sender => sender.SoundData.IsMute = true);
        }

        public static void Unmute(SoundDatabaseName databaseName)
        {
            ApplyWhere(databaseName, controller => controller.Unmute());
            ApplyWhere(databaseName, sender => sender.SoundData.IsMute = false);
        }

        public static void Mute(SoundName soundName)
        {
            ApplyWhere(soundName, controller => controller.Mute());
            ApplyWhere(soundName, sender => sender.SoundData.IsMute = true);
        }

        public static void Unmute(SoundName soundName)
        {
            ApplyWhere(soundName, controller => controller.Unmute());
            ApplyWhere(soundName, sender => sender.SoundData.IsMute = false);
        }

        public static void Pause(SoundName soundName) =>
            ApplyWhere(soundName, controller => controller.Pause());

        public static void UnPause(SoundName soundName) =>
            ApplyWhere(soundName, controller => controller.Unpause());

        public static void Pause(SoundDatabaseName databaseName) =>
            ApplyWhere(databaseName, controller => controller.Pause());

        public static void UnPause(SoundDatabaseName databaseName) =>
            ApplyWhere(databaseName, controller => controller.Unpause());

        public static void DestroyAll()
        {
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
                controller.Destroy();
        }

        public static void ChangeVolume(SoundDatabaseName databaseName, float volume)
        {
            ApplyWhere(databaseName, controller => controller.SetVolume(volume));
            ApplyWhere(databaseName, sender => sender.SoundData.Volume = volume);
        }

        public static IEnumerable<DeepSoundSender> GetSenders(SoundDatabaseName databaseName)
        {
            List<DeepSoundSender> senders = Instance._cachedSenders;
            senders.Clear();
            
            foreach (DeepSoundSender sender in Instance._senders)
            {
                if (sender.SoundData.DatabaseName != databaseName)
                    continue;
                
                senders.Add(sender);
            }
            
            return senders;
        }

        public static IEnumerable<DeepSoundController> GetControllers(SoundName soundName)
        {
            List<DeepSoundController> controllers = Instance._cachedControllers;
            controllers.Clear();
            
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
            {
                if (controller.Name != soundName)
                    continue;
                
                controllers.Add(controller);
            }
            
            return controllers;
        }

        public static IEnumerable<DeepSoundController> GetControllers(SoundDatabaseName soundDatabaseName)
        {
            List<DeepSoundController> cachedControllers = Instance._cachedControllers;
            cachedControllers.Clear();
            IEnumerable<SoundDataBase> dataBases = Database.GetSoundDatabases();
            IReadOnlyList<DeepSoundController> controllers = Instance._pool.AllControllers;
            
            foreach (SoundDataBase dataBase in dataBases)
            {
                if (dataBase.Name != soundDatabaseName)
                    continue;

                foreach (SoundName soundName in dataBase.DataBase.Keys)
                {
                    foreach (DeepSoundController controller in controllers)
                    {
                        if (controller.Name != soundName)
                            continue;
                        
                        cachedControllers.Add(controller);
                    }
                }
            }
            
            return cachedControllers;
        }

        public static IEnumerable<DeepSoundController> GetControllers(IEnumerable<SoundDatabaseName> soundDatabaseNames)
        {
            //TODO зарефакторить
            List<DeepSoundController> cachedControllers = Instance._cachedControllers;
            cachedControllers.Clear();
            IEnumerable<SoundDataBase> dataBases = Database.GetSoundDatabases();
            IReadOnlyList<DeepSoundController> controllers = Instance._pool.AllControllers;
            
            foreach (SoundDataBase dataBase in dataBases)
            {
                if (IsValid(dataBase) == false)
                    continue;

                foreach (SoundName soundName in dataBase.DataBase.Keys)
                {
                    foreach (DeepSoundController controller in controllers)
                    {
                        if (controller.Name != soundName)
                            continue;
                        
                        cachedControllers.Add(controller);
                    }
                }
            }
            
            return cachedControllers;

            bool IsValid(SoundDataBase dataBase)
            {
                foreach (SoundDatabaseName name in soundDatabaseNames)
                {
                    if (dataBase.Name != name)
                        continue;

                    return true;
                }

                return false;
            }
        }

        public static void StopAll()
        {
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
                controller.Stop();
        }

        public static void MuteAll()
        {
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
                controller.Mute();

            foreach (DeepSoundSender sender in Instance._senders)
                sender.SoundData.IsMute = true;
            
            IsMuteAllControllers = true;
        }

        public static void UnmuteAll()
        {
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
                controller.Unmute();
            
            foreach (DeepSoundSender sender in Instance._senders)
                sender.SoundData.IsMute = false;
            
            IsMuteAllControllers = false;
        }

        public static void PauseAll()
        {
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
                controller.Pause();
            
            IsPauseAllControllers = true;
        }

        public static void UnpauseAll()
        {
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
                controller.Unpause();
            
            IsPauseAllControllers = false;
        }

        private static void ApplyWhere(SoundName soundName, Action<DeepSoundController> action)
        {
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
            {
                if (controller.Name != soundName)
                    continue;
                
                action.Invoke(controller);
            }
        }

        private static void ApplyWhere(SoundDatabaseName databaseName, Action<DeepSoundController> action)
        {
            foreach (DeepSoundController controller in Instance._pool.AllControllers)
            {
                if (controller.DataBaseName != databaseName)
                    continue;
                
                action.Invoke(controller);
            }
        }

        private static void ApplyWhere(SoundName soundName, Action<DeepSoundSender> action)
        {
            foreach (DeepSoundSender sender in Instance._senders)
            {
                if (sender.SoundData.SoundName != soundName)
                    continue;
                
                action.Invoke(sender);
            }
        }

        private static void ApplyWhere(SoundDatabaseName databaseName, Action<DeepSoundSender> action)
        {
            foreach (DeepSoundSender sender in Instance._senders)
            {
                if (sender.SoundData.DatabaseName != databaseName)
                    continue;
                
                action.Invoke(sender);
            }
        }
    }
}