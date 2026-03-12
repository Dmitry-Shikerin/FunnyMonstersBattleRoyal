using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Sequences.Handlers
{
    public class MySequenceLoopTaskHandler
    {
        public async UniTask FromNumber(
            CancellationToken token, 
            Func<CancellationToken, UniTask> task,
            int number)
        {
            for (int i = 0; i < number; i++)
                await task.Invoke(token);
        }

        public async UniTask FromLoop(
            CancellationToken token, 
            Func<CancellationToken, UniTask> task)
        {
            while (token.IsCancellationRequested == false)
                await task.Invoke(token);
        }
    }
}