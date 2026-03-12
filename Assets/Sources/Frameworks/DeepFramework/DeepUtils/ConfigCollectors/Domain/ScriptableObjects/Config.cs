using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects
{
    public class Config : SerializedScriptableObject
    {
        [FormerlySerializedAs("_enable")]
        [EnumToggleButtons] [HideLabel] [UsedImplicitly]
        [SerializeField] private EnableState enableState = EnableState.Off;
        [EnableIf(nameof(enableState), EnableState.On)]
        [SerializeField] private string _id;
        [EnableIf(nameof(enableState), EnableState.On)]
        [SerializeField] private ScriptableObject _parent;
        
        public ScriptableObject Parent => _parent;
        public string Id => _id;
        
        public void SetId(string id) =>
            _id = id;
        
        public void SetParent(ScriptableObject parent) =>
            _parent = parent;
    }
}