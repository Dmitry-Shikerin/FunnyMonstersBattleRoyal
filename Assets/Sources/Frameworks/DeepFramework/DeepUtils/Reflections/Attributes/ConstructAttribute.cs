using System;
using JetBrains.Annotations;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes
{
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Method)]
    public class ConstructAttribute : Attribute
    {
    }
}