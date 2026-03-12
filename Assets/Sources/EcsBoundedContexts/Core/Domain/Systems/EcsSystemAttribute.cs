using System;

namespace Sources.EcsBoundedContexts.Core.Domain.Systems
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EcsSystemAttribute : Attribute
    {
        public EcsSystemAttribute(int executionOrder)
        {
            ExecutionOrder = executionOrder;
        }


        public int ExecutionOrder { get; }
    }
}