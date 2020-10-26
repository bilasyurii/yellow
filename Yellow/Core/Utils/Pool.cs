using System.Collections.Generic;

namespace Yellow.Core.Utils
{
    public class Pool<T> : IPool where T : class, new()
    {
        private readonly List<T> objects;

        public Pool(int startingCapacity = 10)
        {
            objects = new List<T>(startingCapacity);
        }

        public bool IsEmpty
        {
            get
            {
                return objects.Count != 0;
            }
        }

        public T Get()
        {
            var index = objects.Count - 1;
            
            if (index < 0)
            {
                return new T();
            }
            else
            {
                var obj = objects[index];

                objects.RemoveAt(index);

                return obj;
            }
        }

        public void Populate(int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                objects.Add(new T());
            }
        }

        public void Add(T obj)
        {
            objects.Add(obj);
        }

        public void Add(object obj)
        {
            objects.Add((T)obj);
        }

        object IPool.Get()
        {
            return Get();
        }
    }
}
