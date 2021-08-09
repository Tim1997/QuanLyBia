using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Threading;

namespace Manager8Bia.Helpers
{
    public class ClockHelper
    {
        readonly DispatcherTimer dispatcher;
        readonly Clock clock;

        public ClockHelper()
        {
            dispatcher = new DispatcherTimer();
            clock = new Clock();
            
        }
        public void Start(Action action, int seconds = -1)
        {
            if(seconds == -1)
                dispatcher.Interval = new TimeSpan(0, 1, 0);
            else
                dispatcher.Interval = new TimeSpan(0, 0, seconds);

            dispatcher.Tick += (s, e) =>
            {
                clock.Minutes++;

                if (clock.Minutes == 60)
                {
                    clock.Minutes = 0;
                    clock.Hours++;
                }
                action?.Invoke();
            };

            dispatcher.Start();
        }

        public void Close()
        {
            dispatcher.Stop();
        }

        public new string ToString
        {
            get => clock.ToString;
        }
    }

    public class Clock
    {
        public double Hours { get; set; } = 0;
        public double Minutes { get; set; } = 0;
        public double Seconds { get; set; } = 0;

        public new string ToString
        {
            get
            {
                var h = Hours < 10 ? "0" + Hours.ToString() : Hours.ToString();
                var m = Minutes < 10 ? "0" + Minutes.ToString() : Minutes.ToString();
                //var s = Seconds < 10 ? "0" + Seconds.ToString() : Seconds.ToString();
                return $"{h}:{m}";
            }
        }
    }
}
