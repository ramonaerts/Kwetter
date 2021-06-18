using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Messaging
{
    public static class QueuedTasks
    {
        private static bool _taskCompleted = true;
        private static ConcurrentBag<Action> _taskCollection = new ConcurrentBag<Action>();

        /// <summary>
        /// Store a task that failed to execute due to an external service not reachable
        /// </summary>
        /// <param name="methodToExecute">The task to execute</param>
        public static void QueueMethod(Action methodToExecute)
        {
            _taskCollection.Add(methodToExecute);
        }

        public static void ExecuteAction()
        {
            if (!_taskCollection.Any() || !_taskCompleted)
            {
                return;
            }

            _taskCompleted = false;
            var failedActions = new ConcurrentBag<Action>();
            foreach (var action in _taskCollection)
            {
                try
                {
                    action.DynamicInvoke();
                }
                catch (Exception)
                {
                    failedActions.Add(action);
                }
            }

            _taskCollection.Clear();
            _taskCollection = failedActions;
            _taskCompleted = true;
        }
    }
}
