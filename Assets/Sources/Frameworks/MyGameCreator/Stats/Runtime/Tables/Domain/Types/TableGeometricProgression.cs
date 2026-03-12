using System;
using Sources.Frameworks.DeepFramework.DeepUtils.Images.Bytes;
using Sources.Frameworks.MyGameCreator.Core.Runtime.Common;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Stats.Runtime.Tables.Domain.Types
{
    [Image(typeof(IconTable), ColorTheme.Type.Red)]
    [Serializable]
    public class TableGeometricProgression : TTable
    {
        private const float ZERO = 0.0001f;
        
        // +--------------------------------------------------------------------------------------+
        // | EXP_Level(n + 1) = EXP_Level(n) * rate                                               |
        // |                                                                                      |
        // | n: is the current level.                                                             |
        // | rate: the incremental ratio of experience from the previous level                    |
        // +--------------------------------------------------------------------------------------+
        
        [SerializeField] private int _maxLevel = 99;
        [SerializeField] private int _increment = 50;
        [SerializeField] private float _rate = 1.05f;
        
        public override int MinLevel => 1;
        public override int MaxLevel => _maxLevel;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public TableGeometricProgression() : base()
        { }

        public TableGeometricProgression(int maxLevel, int increment, float rate) : this()
        {
            _maxLevel = maxLevel;
            _increment = increment;
            _rate = rate;
        }
        
        // IMPLEMENT METHODS: ---------------------------------------------------------------------

        protected override int LevelFromCumulative(int cumulative)
        {
            float value = ((float) cumulative + _increment + 1f) * (_rate - 1f);
            float result = Mathf.Log(value / _increment + 1f, _rate);
            
            return Mathf.FloorToInt(result);
        }

        protected override int CumulativeFromLevel(int level)
        {
            float value = (Mathf.Pow(_rate, level) - 1f) / (_rate - 1f);
            return Mathf.FloorToInt(_increment * value) - _increment;
        }
    }
}