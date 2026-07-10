using Leopotam.EcsProto;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using Reflex.Core;
using Reflex.Injectors;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections;

namespace Sources.EcsBoundedContexts.Common.Extansions.Colliders
{
    public static class NodeCanvasExtension
    {
        public static void ConstructFsm<T>(this GraphOwner<T> owner, params object[] dependencies)
            where T : Graph
        {
            foreach (var state in owner.behaviour.GetAllNodesOfType<FSMState>())
                ReflectionUtils.ResolveDependencies(state, dependencies);
            
            foreach (var task in owner.behaviour.GetAllTasksOfType<Task>())
                ReflectionUtils.ResolveDependencies(task, dependencies);
            
            foreach (var graph in owner.behaviour.GetAllNestedGraphs<BehaviourTree>(true))
            {
                foreach (var task in graph.GetAllTasksOfType<Task>())
                    ReflectionUtils.ResolveDependencies(task, dependencies);
            }
        }
        
        public static void InitGraphOwner<T>(
            this GraphOwner<T> owner, Container container, ProtoEntity entity, params object[] dependencies)
            where T : Graph
        {
            T behaviour = owner.behaviour;
            //fsm.preInitializeSubGraphs = true;
            //fsm.Initialize();

            if (owner is FSMOwner)
                entity.AddFsmOwner(owner as FSMOwner);
            else if (owner is BehaviourTreeOwner)
                entity.AddBehaviourTreeOwner(owner as BehaviourTreeOwner);
            
            behaviour.Initialize(behaviour.agent, behaviour.blackboard, true, false);
            owner.ConstructFsm(entity, dependencies);
            owner.InjectOwner(container);
            owner.StartBehaviour();
        }

        private static void InjectOwner<T>(this GraphOwner<T> owner, Container container)
            where T : Graph
        {
            foreach (FSMState state in owner.behaviour.GetAllNodesOfType<FSMState>())
                AttributeInjector.Inject(state, container);
            
            foreach (Task task in owner.behaviour.GetAllTasksOfType<Task>())
                AttributeInjector.Inject(task, container);
            
            foreach (var graph in owner.behaviour.GetAllNestedGraphs<BehaviourTree>(true))
            {
                foreach (var task in graph.GetAllTasksOfType<Task>())
                    AttributeInjector.Inject(task, container);
            }
        }
    }
}