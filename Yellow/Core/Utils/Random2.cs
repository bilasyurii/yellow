using System;
using System.Collections.Generic;

namespace Yellow.Core.Utils
{
    public class Random2 : Random
    {
        public int Seed { get; private set; }

        public Random2() : base() {}

        public Random2(int seed) : base(seed)
        {
            Seed = seed;
        }

        public float Get()
        {
            return (float)Sample();
        }

        public float Get(float max)
        {
            return max * (float)Sample();
        }

        public float Get(float min, float max)
        {
            return min + (max - min) * (float)Sample();
        }

        public Vec2 Vector()
        {
            return new Vec2((float)Sample() * 2.0f - 1.0f, (float)Sample() * 2.0f - 1.0f);
        }

        public Vec2 Vector(float length)
        {
            return new Vec2(((float)Sample() * 2.0f - 1.0f) * length, ((float)Sample() * 2.0f - 1.0f) * length);
        }

        public T Get<T>(IList<T> collection)
        {
            return collection[Next(collection.Count)];
        }
    }
}
