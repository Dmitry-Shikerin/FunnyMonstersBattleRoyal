using System.Collections.Generic;
using Sources.Frameworks.MyGameCreator.Core.Runtime.Common;
using Sources.Frameworks.MyGameCreator.Stats.Runtime.Attributes.Domain;
using Sources.Frameworks.MyGameCreator.Stats.Runtime.States.Domain;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Stats.Runtime.Classes
{
    [CreateAssetMenu(
        fileName = "Class", 
        menuName = "Game Creator/Stats/Class",
        order = 50)]
    
    public class Class : ScriptableObject
    {
        [SerializeField] private string _id = "ID";
        [SerializeField] private string _description = "Description";
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;
        [SerializeField] private List<Attribute> _attributes = new List<Attribute>();
        [SerializeField] private List<Stat> _stats = new List<Stat>();
        
        public string ID => _id;
        public string Description => _description;
        public Sprite Sprite => _sprite;
        public Color Color => _color;
        public List<Attribute> Attributes => _attributes;
        public List<Stat> Stats => _stats;
    }
}