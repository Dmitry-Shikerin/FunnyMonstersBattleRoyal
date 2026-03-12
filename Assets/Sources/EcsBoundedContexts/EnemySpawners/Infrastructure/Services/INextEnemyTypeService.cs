using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;

namespace Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Services
{
    public interface INextEnemyTypeService
    {
        EnemyType GetNextEnemyType(ProtoEntity spawnEntity);
    }
}