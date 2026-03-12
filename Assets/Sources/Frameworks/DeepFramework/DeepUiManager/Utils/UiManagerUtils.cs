using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUtils.CodeGen;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Utils
{
    public static class UiManagerUtils
    {
        public static void GenerateActionsIds()
        {
            EnumGenerator.SetName("UiActionId");
            EnumGenerator.SetPath("Sources/Frameworks/UiManagers/Domain/Enums");
            List<string> names = ReflectionUtils
                .GetFilteredTypeList<IUiAction>()
                .Select(type => type.Name)
                .ToList();
            EnumGenerator.SetEnumNames(names);
            EnumGenerator.Generate();
        }
    }
}