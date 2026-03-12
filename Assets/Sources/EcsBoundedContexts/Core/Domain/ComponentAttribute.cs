using System;

namespace Sources.EcsBoundedContexts.Core.Domain
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class ComponentAttribute : Attribute
    {
        public ComponentAttribute(
            AspectName aspect = AspectName.Game, 
            ComponentGroup group = ComponentGroup.Common)
        {
            Aspect = aspect;
            Group = group;
        }

        public AspectName Aspect { get; }
        public ComponentGroup Group { get; }
    }
}