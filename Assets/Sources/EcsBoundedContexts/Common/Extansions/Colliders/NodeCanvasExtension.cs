using Leopotam.EcsProto;
using MyDependencies.Sources.Containers;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
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
            this GraphOwner<T> owner, DiContainer container, ProtoEntity entity, params object[] dependencies)
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
            owner.InjectFsm(container);
            owner.StartBehaviour();
        }

        private static void InjectFsm<T>(this GraphOwner<T> owner, DiContainer container)
            where T : Graph
        {
            foreach (FSMState state in owner.behaviour.GetAllNodesOfType<FSMState>())
                container.Inject(state);
            
            foreach (Task task in owner.behaviour.GetAllTasksOfType<Task>())
                container.Inject(task);
            
            foreach (var graph in owner.behaviour.GetAllNestedGraphs<BehaviourTree>(true))
            {
                foreach (var task in graph.GetAllTasksOfType<Task>())
                    container.Inject(task);
            }
        }
    }
}