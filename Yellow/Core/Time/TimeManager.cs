using SFML.System;
using System.Collections.Generic;

namespace Yellow.Core.Time
{
    public class TimeManager
    {
        private readonly Clock clock = new Clock();

        private readonly Game game;

        private readonly List<Timer> timers = new List<Timer>();

        public int Now { get; private set; } = 0;

        public float DeltaTime { get; private set; } = 0;

        public int DeltaTimeMS { get; private set; } = 0;

        public Timer Events { get; private set; }

        public TimeManager(Game game)
        {
            this.game = game;
            Events = new Timer(game);
            clock.Restart();
        }

        public Timer MakeTimer()
        {
            var timer = new Timer(game);

            timers.Add(timer);

            return timer;
        }

        public Timer Add(Timer timer)
        {
            timers.Add(timer);

            return timer;
        }

        public void Update()
        {
            var newNow = clock.ElapsedTime.AsMilliseconds();
            DeltaTimeMS = newNow - Now;
            Now = newNow;
            DeltaTime = DeltaTimeMS * 0.001f;

            Events.Update(newNow);

            foreach (var timer in timers)
            {
                timer.Update(newNow);
            }
        }
    }
}
