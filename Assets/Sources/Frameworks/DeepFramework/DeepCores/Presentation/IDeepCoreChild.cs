using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepCores.Presentation
{
    public interface IDeepCoreChild
    {
        GameObject GameObject { get; }
        
        void Destroy();
    }
}