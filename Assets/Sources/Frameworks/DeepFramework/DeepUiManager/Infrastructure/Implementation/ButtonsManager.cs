using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Implementation;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation
{
    public class ButtonsManager
    {
        private readonly StreamSignalBus _signalBus;
        private readonly Dictionary<ButtonId, List<UiButton>> _buttons = new ();

        public ButtonsManager(StreamSignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Register(ButtonId buttonId, UiButton button)
        {
            if (_buttons.ContainsKey(buttonId) == false)
                _buttons.Add(buttonId, new List<UiButton>());
            
            _buttons[buttonId].Add(button);
        }
        
        public void Unregister(ButtonId buttonId, UiButton button)
        {
            if (_buttons.ContainsKey(buttonId) == false)
                return;
            
            _buttons[buttonId].Remove(button);
        }

        public List<UiButton> GetButton(ButtonId buttonId) =>
            _buttons[buttonId];

        public void HandleOnClick(ButtonId buttonId) =>
            _signalBus.Handle(new OnClickSignal(buttonId));

        public void Destroy() =>
            _buttons.Clear();
    }
}