using System;

namespace Assignment7
{
    public interface IClock
    {
        void Start();

        event EventHandler<TickArg> OnTick;

        TimeSpan Interval { get; set; }

    }
}