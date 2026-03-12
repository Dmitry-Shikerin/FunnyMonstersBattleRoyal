using System;
using System.Collections.Generic;
using MyDependencies.Sources.Containers;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Implementation;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation
{
    public class UiActionHandler
    {
        private readonly ActionHandler _actionHandler;
        private readonly StreamSignalBus _signalBus;

        public UiActionHandler(StreamSignalBus signalBus)
        {
            _actionHandler = new ActionHandler();
            _signalBus = signalBus;
        }

        public void Initialize(DiContainer container)
        {
            foreach (IUiAction action in GetActions())
                container.Inject(action);
            
            _actionHandler.Initialize();
            _signalBus.Subscribe<UiActionSignal>(Handle);
        }

        public void Destroy()
        {
            _signalBus.Unsubscribe<UiActionSignal>(Handle);
            _actionHandler.Destroy();
        }

        public void AddAction<T>()
            where T : IUiAction
        {
            IUiAction uiAction = Activator.CreateInstance(typeof(T)) as IUiAction;
            AddAction(uiAction);
        }

        public void AddAction(IUiAction uiAction) =>
            _actionHandler.Add(uiAction);

        public IEnumerable<IUiAction> GetActions() =>
            _actionHandler.GetActions();

        public void Handle(IEnumerable<UiActionId> actionIds) =>
            _signalBus.Handle(new UiActionSignal(actionIds));

        private void Handle(UiActionSignal signal) =>
            _actionHandler.Handle(signal.ActionIds);
    }
}