using System;
using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Interfaces;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation
{
    public class ActionHandler : IActionHandler
    {
        private readonly Dictionary<UiActionId, IUiAction> _commands = new();

        public void Initialize()
        {
            foreach (IUiAction command in _commands.Values)
                command.Initialize();
        }

        public void Add(IUiAction uiAction)
        {
            UiActionId id = uiAction.Id;

            if (_commands.ContainsKey(id))
                throw new InvalidOperationException(id.ToString());

            _commands[id] = uiAction;
        }

        public void Handle(UiActionId id)
        {
            if (_commands.TryGetValue(id, out IUiAction command) == false)
                throw new KeyNotFoundException(id.ToString());

            command.Handle();
        }

        public void Handle(IEnumerable<UiActionId> ids)
        {
            foreach (UiActionId id in ids)
            {
                if (_commands.TryGetValue(id, out IUiAction command) == false)
                    throw new KeyNotFoundException(id.ToString());

                command.Handle();
            }
        }

        public IEnumerable<IUiAction> GetActions() =>
            _commands.Values;

        public void Destroy()
        {
            foreach (IUiAction command in _commands.Values)
                command.Destroy();

            _commands.Clear();
        }
    }
}