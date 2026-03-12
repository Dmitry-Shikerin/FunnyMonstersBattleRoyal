using Sources.EcsBoundedContexts.Core.Domain;
using TMPro;

namespace Sources.EcsBoundedContexts.Damage.Domain
{
    [Component(group: ComponentGroup.Common)]
    public struct HealthTextComponent
    {
        public TMP_Text Value;
    }
}