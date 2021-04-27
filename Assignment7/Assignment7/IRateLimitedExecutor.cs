using System;

namespace Assignment7
{
    public interface IRateLimitedExecutor
    {
        void Enqueue(Action action);
    }
}