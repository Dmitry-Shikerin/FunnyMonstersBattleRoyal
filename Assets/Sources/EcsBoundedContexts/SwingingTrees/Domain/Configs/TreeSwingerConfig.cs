using UnityEngine;

namespace Sources.EcsBoundedContexts.SwingingTrees.Domain.Configs
{
    public class TreeSwingerConfig : ScriptableObject
    {
        [field: Header("Speed settings")]
        [field: Tooltip("How fast do the trees swing in the X axis")]
        [field: Range(0.001f,3f)]
        [field: SerializeField] public float SwingSpeedX { get; private set; }
        [field: Tooltip("The difference in swing speed of each tree in the X axis")]
        [field: Range(0,1f)]
        [field: SerializeField] public float SwingSpeedRandomnessX { get; private set; }
		
        [field: Tooltip("How fast do the trees swing in the Y axis")]
        [field: Range(0.001f,3f)]
        [field: SerializeField] public float SwingSpeedY { get; private set; }
		
        [field: Tooltip("The difference in swing speed of each tree in the Y axis")]
        [field: Range(0,1f)]
        [field: SerializeField] public float SwingSpeedRandomnessY { get; private set; }
		
        [field: Header("Angle settings")]
        [field: Tooltip("How far do the trees swing in the X axis")]
        [field: Range(0.001f,20f)]
        [field: SerializeField] public float SwingMaxAngleX { get; private set; }
        [field: Tooltip("The difference in how far does each trees swing in the X axis")]
        [field: Range(0.001f,5f)]
        [field: SerializeField] public float SwingMaxAngleRandomnessX { get; private set; }
		
        [field: Tooltip("How far do the trees swing in the Y axis")]
        [field: Range(0.001f,180f)]
        [field: SerializeField] public float SwingMaxAngleY { get; private set; }
        [field: Tooltip("The difference in how far does each trees swing in the Y axis")]
        [field: Range(0.001f,15f)]
        [field: SerializeField] public float SwingMaxAngleRandomnessY { get; private set; }

        [field: Header("Direction settings")]
        [field: Tooltip("The \"wind\" direction in angles from standard X axis")]
        [field: Range(0f,180f)]
        [field: SerializeField] public float Direction { get; private set; }
        [field: Tooltip("The \"wind\" direction randomness")]
        [field: Range(0f,180f)]
        [field: SerializeField] public float DirectionRandomness { get; private set; }
        [field: SerializeField] public bool EnableYAxisSwinging { get; private set; }
        
        [HideInInspector] public TreeSwingerCollector Parent;
        [HideInInspector] public string Id;
    }
}