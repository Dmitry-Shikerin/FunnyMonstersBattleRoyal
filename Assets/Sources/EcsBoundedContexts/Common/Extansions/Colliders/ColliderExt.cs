using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Common.Extansions.Colliders
{
    public static class ColliderExt
    {
        private static IEntityRepository s_repository;
        
        public static void Construct(IEntityRepository repository)
        {
            s_repository = repository;
        }
        
        // public static bool IsPlayer(this Collider other)
        // {
        //     return other.IsOnLayer(LayerConst.Player) && s_repository.HasByHash<PlayerTag>(other.gameObject);
        // }

        public static bool IsOnLayer(this Collider other, int layerMask)
        {
            return 1 << other.gameObject.layer == layerMask;
        }
    }
}