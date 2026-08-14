using Fusion;

namespace Sources.Frameworks.ViewComponents.Presentation
{
    public interface IViewComponent
    {
        PlayerRef PlayerRef { get; }

        void Init(PlayerRef playerRef);
    }
}