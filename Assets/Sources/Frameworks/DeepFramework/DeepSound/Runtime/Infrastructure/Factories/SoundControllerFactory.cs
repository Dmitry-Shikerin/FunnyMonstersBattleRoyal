using System;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure.Factories
{
    public class SoundControllerFactory
    {
        private readonly DeepSoundManager _manager;
        private readonly DeepSoundControllersPool _pool;

        public SoundControllerFactory(DeepSoundManager manager, DeepSoundControllersPool pool)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }

        public DeepSoundController Create()
        {
            DeepSoundController controller = new GameObject(
                nameof(DeepSoundController), 
                typeof(AudioSource),
                typeof(DeepSoundController))
                .GetComponent<DeepSoundController>();
            controller.Construct(_manager, _pool);
            
            return controller;
        }
    }
}