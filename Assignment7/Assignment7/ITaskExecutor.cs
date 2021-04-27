using System;
using System.Threading.Tasks;

namespace Assignment7
{
    public interface ITaskExecutor
    {
        Task Execute(Action action);
    }
}