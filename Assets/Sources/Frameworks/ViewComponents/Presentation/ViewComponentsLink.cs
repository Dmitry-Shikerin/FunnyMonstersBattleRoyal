using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Reflex.Core;
using Reflex.Injectors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.Frameworks.ViewComponents.Presentation
{
    public class ViewComponentsLink : MonoBehaviour
    {
        [Required] [SerializeField] private List<MonoBehaviour> _viewComponents;

        public PlayerRef PlayerRef { get; private set; }
        
        private void Awake()
        {
            foreach (MonoBehaviour viewComponent in _viewComponents)
            {
                if (viewComponent == null)
                    throw new NullReferenceException($"Null view component in {gameObject.name}");

                if (viewComponent is not IViewComponent concrete)
                    throw new InvalidOperationException($"This component is not IViewComponent {viewComponent.name} in {gameObject.name}");
            }
        }

        public void Init(PlayerRef playerRef, Container container)
        {
            PlayerRef = playerRef;

            foreach (MonoBehaviour component in _viewComponents)
            {
                if (component is not IViewComponent concrete)
                    throw new InvalidOperationException($"This component is not IViewComponent {component.name} in {gameObject.name}");
                
                concrete.Init(playerRef);
                AttributeInjector.Inject(component, container);
            }
        }

        public T Get<T>()
            where T : IViewComponent
        {
            foreach (MonoBehaviour component in _viewComponents)
            {
                if (component is not T concrete)
                    continue;

                return concrete;
            }

            throw new NullReferenceException();
        }
        
        [Button]
        private void Fill()
        {
            _viewComponents.Clear();
            List<MonoBehaviour> monoBehaviours = gameObject.GetComponents<MonoBehaviour>().ToList();
            IEnumerable<MonoBehaviour> result = monoBehaviours.Concat(gameObject.GetComponentsInChildren<MonoBehaviour>().ToList());
            
            foreach (MonoBehaviour monoBehaviour in result)
            {
                if (monoBehaviour is not IViewComponent concrete)
                    continue;

                _viewComponents.Add(monoBehaviour);
            }
            
        }
    }
}