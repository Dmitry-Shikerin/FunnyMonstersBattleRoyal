using System;
using Sources.Frameworks.MyGameCreator.Stats.Runtime.Tables.Domain;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Stats.Runtime.States
{
    [Serializable]
    public class StatData
    {
       [SerializeField] private double _baseValue = 0;
       [SerializeField] private Table _table;
        
        public double BaseValue => _baseValue;
        public Table Table => _table != null ? _table : throw new NullReferenceException();
    }
}