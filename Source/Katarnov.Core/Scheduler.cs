using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    internal static class Scheduler
    {
        static Queue<UpdateTaskInfo> queryQueue = new Queue<UpdateTaskInfo>();
        static Queue<UpdateTaskInfo> bufferQueryQueue = new Queue<UpdateTaskInfo>();
        
        internal static void Enqueue(UpdateTaskInfo taskinfo)
        {
            bufferQueryQueue.Enqueue(taskinfo);
        }

        internal static void Enqueue(IUpdatable updatable)
        {
            Enqueue(new UpdateTaskInfo(updatable));
        }

        internal static void ProcessUpdates()
        {
            queryQueue = new Queue<UpdateTaskInfo>(bufferQueryQueue);
            bufferQueryQueue.Clear();

            List<Task> tasks = new List<Task>(); 

            while (queryQueue.Count > 0)
            {
                var info = queryQueue.Dequeue();

                tasks.Add(Task.Run(() => DoUpdateTask(info)));
            }

            Task.WaitAll(tasks.ToArray());

            queryQueue.Clear();
            queryQueue = null;
        } 

        private static void DoUpdateTask(UpdateTaskInfo info)
        {
            info.Object.Update();
        }
    }
}
