using System;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Gizmoses.Presentation;
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation
{
    [Serializable]
    public class CharacterGizmosDrawer : DebugGizmosDrawer
    {
        private const string FindRangeGroup = "FindRange";
        
        [SerializeField] private Config _config;
        [PropertySpace(5)] [BoxGroup(FindRangeGroup)]
        [SerializeField] private Color _color;
        
        public override void Draw(GameObject obj)
        {
            Gizmos.color = _color;
            float radius = GetRadius();
            Gizmos.DrawSphere(obj.transform.position, radius);
        }

        private float GetRadius()
        {
            Type type = _config.GetType();
            
            if (type == typeof(CharacterMeleeConfig))
                return ((CharacterMeleeConfig)_config).FindRange;
            
            if (type == typeof(CharacterRangeConfig))
                return ((CharacterRangeConfig)_config).FindRange;
            
            throw new InvalidOperationException();
        }
    }
}