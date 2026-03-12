using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.BoundedContexts.RootGameObjects.Presentation
{
    public class MainMenuRootGameObjects : MonoBehaviour
    {
        private const string CameraFolder = "Camera";
        
        [field: FoldoutGroup(CameraFolder)]
        [field: SerializeField] public Camera MainCamera { get; private set; }        
        [field: SerializeField] public GameObject BlackScreen { get; private set; }
    }
}