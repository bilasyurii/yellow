using System;

namespace Yellow.Core.Time
{
    public delegate void EventCallback(EventArgs args);

    public class TimerEvent
    {
        public int delay;

        public int time;

        public bool loop;

        public int repeats;

        public bool deletePending;

        public EventCallback callback;

        public EventArgs arguments;

        public void Raise()
        {
            callback(arguments);
        }
    }
}
