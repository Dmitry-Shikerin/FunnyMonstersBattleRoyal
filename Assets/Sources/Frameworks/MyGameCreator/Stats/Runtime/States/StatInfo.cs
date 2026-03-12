using System;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Stats.Runtime.States
{
    [Serializable]
    public class StatInfo
    {
        [SerializeField] private string _acronym;
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        public string Acronym => _acronym;
        public string Name => _name;
        public string Description => _description;
    }
}