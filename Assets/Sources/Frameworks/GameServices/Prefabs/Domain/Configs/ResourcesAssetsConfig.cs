using Sources.EcsBoundedContexts.Characters.Presentation;
using UnityEngine;

namespace Sources.Frameworks.GameServices.Prefabs.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(ResourcesAssetsConfig), menuName = "Configs/" + nameof(ResourcesAssetsConfig), order = 51)]
    public class ResourcesAssetsConfig : ScriptableObject
    {
        [SerializeField] private AssetContainer<CharacterRangeModule> _asset;
    }
}