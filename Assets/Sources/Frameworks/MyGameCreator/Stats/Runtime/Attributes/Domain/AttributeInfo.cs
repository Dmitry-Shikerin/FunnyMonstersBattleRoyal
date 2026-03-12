using System;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Stats.Runtime.Attributes.Domain
{
    [Serializable]
    public class AttributeInfo
    {
        [SerializeField] private string _acronym;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        
        public string Acronym => _acronym;
        public string Name => _name;
        public string Description => _description;
    }
}