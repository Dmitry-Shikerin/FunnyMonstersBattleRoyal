using System;

namespace Sources.EcsBoundedContexts.Core.Domain.Systems
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentGroupAttribute : Attribute
    {
        public ComponentGroupAttribute(ComponentGroup group)
        {
            Group = group;
        }
        
        public ComponentGroup Group { get; }
    }
}