using Sources.Frameworks.GameServices.ObjectPools.Interfaces.Bakers;

namespace Sources.Frameworks.GameServices.EntityPools.Interfaces.Bakers.Generic
{
    public interface IPoolBaker<T> : IPoolBaker
        where T : struct
    {
    }
}