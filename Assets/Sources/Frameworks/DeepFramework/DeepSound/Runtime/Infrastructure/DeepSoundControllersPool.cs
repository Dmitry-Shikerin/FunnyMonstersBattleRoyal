using System;
using System.Collections;
using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepCores.Coroutines;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure.Factories;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure
{
    public class DeepSoundControllersPool
    {
        private readonly Transform _parentTransform;
        private readonly DeepSoundSettings _settings;
        private readonly WaitForSecondsRealtime _interval;
        private readonly List<DeepSoundController> _pool = new ();
        private readonly List<DeepSoundController> _allControllers = new ();
        private readonly List<DeepSoundController> _freedoms = new ();
        
        private Coroutine _idleCheckCoroutine;
        private DeepSoundController _tempController;
        private SoundControllerFactory _factory;

        public DeepSoundControllersPool(
            DeepSoundSettings settings,
            DeepSoundManager manager)
        {
            _parentTransform = manager.transform;
            _settings = settings;
            _interval = new WaitForSecondsRealtime(IdleCheckInterval < 0 ? 0 : IdleCheckInterval);
        }

        private bool IsAutoKillIdle => _settings.AutoKillIdleControllers;
        private float KillDuration => _settings.ControllerIdleKillDuration;
        private float IdleCheckInterval => _settings.IdleCheckInterval;
        public int MinCount => _settings.MinimumNumberOfControllers;
        public IReadOnlyList<DeepSoundController> Pool => _pool;
        public IReadOnlyList<DeepSoundController> Freedoms => _pool;
        public IReadOnlyList<DeepSoundController> AllControllers => _allControllers;


        public void Initialize(SoundControllerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            PopulatePool(DeepSoundSettings.Instance.MinimumNumberOfControllers);
            StartIdleCheckInterval();
        }

        public void Destroy()
        {
            StopIdleCheck();
            ClearPool();
        }

        public void ClearPool(bool keepMinimumNumberOfControllers = false)
        {
            if (keepMinimumNumberOfControllers)
            {
                RemoveNullControllers();
                
                if (_pool.Count <= MinCount)
                    return;

                int killedControllersCount = 0;
                
                for (int i = _pool.Count - 1; i >= MinCount; i--)
                {
                    DeepSoundController controller = _pool[i];
                    _pool.Remove(controller);
                    controller.Destroy();
                    killedControllersCount++;
                }

                return;
            }

            foreach (DeepSoundController controller in _allControllers)
                controller.Destroy();
            
            _pool.Clear();
        }
        
        public DeepSoundController Get()
        {
            RemoveNullControllers();
            DeepSoundController controller;

            if (_pool.Count <= 0)
            {
                controller = _factory.Create();
                _allControllers.Add(controller);
                controller.transform.SetParent(_parentTransform);
                _freedoms.Add(controller);
                return controller;
            }
            
            controller = _pool[0];
            _pool.Remove(controller);
            _freedoms.Add(controller);
            controller.gameObject.SetActive(true);
            
            return controller;
        }

        public void ReturnToPool(DeepSoundController controller)
        {
            if (controller == null) 
                return;

            if (_pool.Contains(controller) == false)
            {
                _freedoms.Remove(controller);
                _pool.Add(controller);
            }
            
            controller.gameObject.SetActive(false);
        }

        private void PopulatePool(int count)
        {
            RemoveNullControllers();
            
            if (count < 1) 
                return;

            for (int i = 0; i < count; i++)
            {
                DeepSoundController controller = _factory.Create();
                _allControllers.Add(controller);
                controller.transform.SetParent(_parentTransform);
                _pool.Add(controller);
            }
        }

        private void StartIdleCheckInterval()
        {
            if (IsAutoKillIdle == false)
                return;
            
            _idleCheckCoroutine = CoroutineRunner.Start(DestroyIdleControllers());
        }

        private void StopIdleCheck()
        {
            if (_idleCheckCoroutine == null) 
                return;
            
            CoroutineRunner.Stop(_idleCheckCoroutine);
            _idleCheckCoroutine = null;
        }
        
        private void RemoveNullControllers()
        {
            for (int i = _pool.Count - 1; i >= 0; i--)
            {
                DeepSoundController controller = _pool[i];
                
                if (controller != null)
                    continue;
                
                _pool.RemoveAt(i);
            }
        }

        private IEnumerator DestroyIdleControllers()
        {
            while (IsAutoKillIdle)
            {
                yield return _interval;
                
                RemoveNullControllers();
                int minCount = MinCount > 0 ? MinCount : 0;
                float killDuration = KillDuration > 0 ? KillDuration : 0;
                
                if (_pool.Count <= minCount) 
                    continue;
                
                for (int i = _pool.Count - 1; i >= minCount; i--)
                {
                    _tempController = _pool[i];
                    
                    if (_tempController.gameObject.activeSelf) 
                        continue;
                    
                    if (_tempController.IdleDuration < killDuration) 
                        continue;
                    
                    _pool.Remove(_tempController);
                    _tempController.Destroy();
                }
            }

            _idleCheckCoroutine = null;
        }
    }
}