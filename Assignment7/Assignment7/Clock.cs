using System;
using System.Timers;

namespace Assignment7
{
    public sealed class Clock : IClock, IDisposable
    {
        private readonly Timer _timer;
        private TimeSpan _interval = new TimeSpan(0, 0, 0, 5);

        public Clock()
        {
            _timer = new Timer
            {
                Interval = Interval.TotalMilliseconds,
                AutoReset = true
            };
            _timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            OnOnTick(new TickArg());
        }

        private void OnOnTick(TickArg e)
        {
            OnTick?.Invoke(this, e);
        }

        public TimeSpan Interval
        {
            get => _interval;
            set
            {
                _interval = value;
                if (_timer != null)
                    _timer.Interval = _interval.TotalMilliseconds;
            }
        }

        public void Start()
        {
            _timer.Start();
        }


        public event EventHandler<TickArg> OnTick;


        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}