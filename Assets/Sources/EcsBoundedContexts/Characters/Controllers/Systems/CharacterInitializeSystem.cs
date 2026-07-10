using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Photon.Pun;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(10)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class CharacterInitializeSystem : IProtoInitSystem
    {
        private readonly IAssetCollector _assetCollector;
        private readonly CharacterEntityFactory _entityFactory;
        private readonly RootGameObject _rootGameObject;

        public CharacterInitializeSystem(
            IAssetCollector assetCollector,
            CharacterEntityFactory entityFactory,
            RootGameObject rootGameObject)
        {
            _assetCollector = assetCollector;
            _entityFactory = entityFactory;
            _rootGameObject = rootGameObject;
        }

        public void Init(IProtoSystems systems)
        {
            //EntityLink characterLink = _rootGameObject.Character;
            CharacterModule module = _assetCollector.Get<CharacterModule>();
            
            if (PhotonNetwork.IsConnected)
            {
                module = PhotonNetwork.Instantiate("Character", Vector3.zero, Quaternion.identity).GetComponent<CharacterModule>();
            }
            else
            {
                module = Object.Instantiate(module, Vector3.zero, Quaternion.identity);
            }
            Debug.Log($"Init character");
            _entityFactory.Create(module.GetComponent<EntityLink>());
        }
    }
}