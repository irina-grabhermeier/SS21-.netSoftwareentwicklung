using System;
using System.Collections.Generic;

namespace Assignment7
{
    public class RateLimitedExecutor : IRateLimitedExecutor
    {

        private readonly ITaskExecutor executor;
        private readonly IClock clock;
        private readonly Queue<Action> toExecutes;

        public RateLimitedExecutor(ITaskExecutor executor, IClock clock, IEnumerable<Action> toQuery = null)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
            this.clock = clock ?? throw new ArgumentNullException(nameof(clock));

            toExecutes = new Queue<Action>(toQuery ?? ArraySegment<Action>.Empty);
            clock.OnTick += ClockOnTick;
            clock.Start();
        }

        private async void ClockOnTick(object? sender, TickArg e)
        {
            if (toExecutes.TryDequeue(out Action toExecute))
                await executor.Execute(toExecute);
        }

        public void Enqueue(Action action)
        {
            toExecutes.Enqueue(action);
        }
    }
}