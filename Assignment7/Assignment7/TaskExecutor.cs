using System;
using System.Threading.Tasks;

namespace Assignment7
{
    public class TaskExecutor : ITaskExecutor
    {
        public Task Execute(Action action)
        {
            action.Invoke();
            return Task.CompletedTask;
        }
    }
}