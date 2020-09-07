using System;
using System.Collections.Generic;

namespace Yellow.Core.Time
{
    public class Timer
    {
        protected readonly Game game;

        protected readonly List<TimerEvent> events = new List<TimerEvent>();

        protected int eventCount = 0;

        protected int nextEventTime;

        protected int startTime;

        protected bool running;

        protected int currentTime;

        public Timer(Game game)
        {
            this.game = game;
        }

        public void Start(int delay = 0)
        {
            if (!running)
            {
                running = true;
                startTime = delay + game.Time.Now;

                for (var i = 0; i < eventCount; ++i)
                {
                    var timerEvent = events[i];

                    timerEvent.time = startTime + timerEvent.delay;
                }
            }
        }

        public void Clear()
        {
            events.Clear();
            eventCount = 0;
        }

        public void Stop()
        {
            running = false;
            Clear();
        }

        public TimerEvent Add(int delay, EventCallback callback, EventArgs args = null)
        {
            return Create(callback, args, delay, false, 0);
        }

        public TimerEvent Repeat(int delay, int repeats, EventCallback callback, EventArgs args = null)
        {
            return Create(callback, args, delay, false, repeats);
        }

        public TimerEvent Loop(int delay, EventCallback callback, EventArgs args = null)
        {
            return Create(callback, args, delay, true, 0);
        }

        public TimerEvent Create(EventCallback callback, EventArgs arguments, int delay, bool loop, int repeats)
        {
            var time = currentTime + delay;

            var timerEvent = new TimerEvent()
            {
                callback = callback,
                arguments = arguments,
                delay = delay,
                loop = loop,
                repeats = repeats,
                time = time
            };

            // add event to list
            var bottom = 0;
            var top = eventCount;
            int middle;

            while (bottom < eventCount)
            {
                middle = (top + bottom) >> 1;

                if (events[middle].time < time)
                {
                    bottom = middle + 1;
                }
                else
                {
                    top = middle;
                }
            }

            events.Insert(bottom, timerEvent);

            return timerEvent;
        }

        public void Update(int time)
        {
            currentTime = time;

            DeletePending();

            if (eventCount != 0 && nextEventTime < time)
            {
                var i = 0;

                var pendingCount = 0;

                while (i < eventCount)
                {
                    var timerEvent = events[i];

                    if (timerEvent.time < time && !timerEvent.deletePending)
                    {
                        if (timerEvent.loop)
                        {
                            timerEvent.time += timerEvent.delay;
                        }
                        else if (timerEvent.repeats != 0)
                        {
                            timerEvent.time += timerEvent.delay;
                            --timerEvent.repeats;
                        }
                        else
                        {
                            timerEvent.deletePending = true;
                            ++pendingCount;
                        }

                        timerEvent.Raise();

                        ++i;
                    }
                    else
                    {
                        break;
                    }
                }

                if (eventCount > pendingCount)
                {
                    events.Sort(TimerEventsComparison);
                    nextEventTime = events[0].time;
                }
            }
        }

        protected virtual int TimerEventsComparison(TimerEvent a, TimerEvent b)
        {
            return a.time - b.time;
        }

        protected void DeletePending()
        {
            for (var i = eventCount - 1; i >= 0; --i)
            {
                if (events[i].deletePending)
                {
                    events.RemoveAt(i);
                }
            }

            eventCount = events.Count;
        }
    }
}
