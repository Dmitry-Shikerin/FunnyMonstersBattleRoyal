using System;
using Sources.Frameworks.StateMachines.ContextStateMachines.Interfaces.Contexts;

namespace Sources.Frameworks.StateMachines.ContextStateMachines.Implementation.States
{
    public class FuncContextState : ContextStateBase
    {
        private readonly Action _enter;
        private readonly Action _exit;
        private readonly Action _update;
        private readonly Action<IContext> _afterApply;

        public FuncContextState(
            Action enter, 
            Action exit, 
            Action update, 
            Action<IContext> afterApply)
        {
            _enter = enter;
            _exit = exit;
            _update = update;
            _afterApply = afterApply;
        }
        
        public override void Enter(object payload = null)
        {
            _enter.Invoke();
        }
        
        public override void Exit()
        {
            _exit.Invoke();
        }
        
        public override void Update(float deltaTime)
        {
            _update.Invoke();
        }
        
        public override void BeforeApply(IContext context)
        {
            _afterApply.Invoke(context);
        }
    }
}