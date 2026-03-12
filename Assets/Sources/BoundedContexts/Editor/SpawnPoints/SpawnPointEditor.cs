using Sources.EcsBoundedContexts.CharacterSpawner.Presentation;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation.Types;
using UnityEditor;
using UnityEngine;

namespace Sources.BoundedContexts.Editor.SpawnPoints
{
    [CustomEditor(typeof(SpawnPointModule))]
    public class SpawnPointEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnPointModule spawner, GizmoType gizmo)
        {
            Gizmos.color = SetColor(spawner);
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }

        private static Color SetColor(SpawnPointModule spawnPoint)
        {
            return spawnPoint.SpawnPointType switch
            {
                SpawnPointType.CharacterMelee => Color.green,
                SpawnPointType.CharacterRanged => Color.blue,
                SpawnPointType.Enemy => Color.red,
                _ => Color.white
            };
        }
    }
}