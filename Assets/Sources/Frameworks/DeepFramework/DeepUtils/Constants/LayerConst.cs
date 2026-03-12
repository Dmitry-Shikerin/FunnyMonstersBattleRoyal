using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Constants
{
    public class LayerConst
    {
        public static readonly int Default = 0;
        public static readonly int Player = 1 << LayerMask.NameToLayer("Player");
        public static readonly int Enemy = 1 << LayerMask.NameToLayer("Enemy");
        public static readonly int Obstacle = 1 << LayerMask.NameToLayer("Obstacle");
    }
}