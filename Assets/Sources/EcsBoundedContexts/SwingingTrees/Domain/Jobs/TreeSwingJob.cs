using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Sources.EcsBoundedContexts.SwingingTrees.Domain.Jobs
{
    public readonly struct TreeSwingJob : IJobParallelForTransform
    {
        private readonly NativeArray<SwingingTreeComponent> _treeSwingers;
        private readonly float _timeTime;

        public TreeSwingJob(NativeArray<SwingingTreeComponent> treeSwingers, float timeTime)
        {
            _treeSwingers = treeSwingers;
            _timeTime = timeTime;
        }

        public void Execute(int index, TransformAccess transform)
        {
            transform.rotation = Quaternion.Euler(
                _treeSwingers[index].MaxAngleX * 
                Mathf.Sin(_timeTime * _treeSwingers[index].SpeedX), 
                _treeSwingers[index].EnableYAxisSwingingTree
                    ? _treeSwingers[index].MaxAngleY * Mathf.Sin(_timeTime * _treeSwingers[index].SpeedY) 
                    : _treeSwingers[index].Direction, 0f);
        }
    }
}