using System;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Enemies.Domain.Configs;
using Sources.EcsBoundedContexts.Gizmoses.Presentation;
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Presentation
{
    [Serializable]
    public class EnemyKamikazeGizmosDrawer : DebugGizmosDrawer
    {
        private const string FindRangeGroup = "FindRange";
        
        [SerializeField] private Config _config;
        [PropertySpace(5)] 
        [BoxGroup(FindRangeGroup)]
        [SerializeField] private Color _findColore;
        [BoxGroup(FindRangeGroup)]
        [SerializeField] private Color _massAttackColore;
        
        public override void Draw(GameObject obj)
        {
            Gizmos.color = _findColore;
            float radius = GetRadius();
            Gizmos.DrawSphere(obj.transform.position, radius);
            
            Gizmos.color = _massAttackColore;
            radius = ((EnemyKamikazeConfig)_config).MassAttackFindRange;
            Gizmos.DrawSphere(obj.transform.position, radius);
        }

        private float GetRadius()
        {
            Type type = _config.GetType();
            
            if (type == typeof(EnemyKamikazeConfig))
                return ((EnemyKamikazeConfig)_config).FindRange;
            
            throw new InvalidOperationException();
        }
    }
}