using System;

namespace Sources.EcsBoundedContexts.Core.Domain.Systems
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class AspectAttribute : Attribute
    {
        public AspectAttribute(params AspectName[] aspects)
        {
            Aspects = aspects;
        }
        
        public AspectName[] Aspects { get; set; }
    }
}