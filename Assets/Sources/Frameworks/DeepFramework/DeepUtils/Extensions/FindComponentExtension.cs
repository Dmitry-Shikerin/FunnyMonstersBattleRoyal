using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Extensions
{
    public static class FindComponentExtension
    {
        public static bool TryFindComponent<T>(this Component component, out T result)
            where T : Component
        {
            if (component.TryGetComponent(out result))
                return true;

            T childrenComponent = component.GetComponentInChildren<T>();
            
            if (childrenComponent != null)
            {
                result = childrenComponent;
                
                return true;
            }

            T parentComponent = component.GetComponentInParent<T>();
            
            if (parentComponent != null)
            {
                result = parentComponent;
                
                return true;
            }

            result = null;
            
            return false;
        } 
    }
}