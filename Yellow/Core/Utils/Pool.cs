using System.Collections.Generic;

namespace Yellow.Core.Utils
{
    public class Pool
    {
        private readonly List<object> objects;

        public Pool(int startingCapacity = 10)
        {
            objects = new List<object>(startingCapacity);
        }

        public bool IsEmpty
        {
            get
            {
                return objects.Count != 0;
            }
        }

        public object Get()
        {
            var index = objects.Count - 1;
            var obj = objects[index];

            objects.RemoveAt(index);

            return obj;
        }

        public T Get<T>() where T : class
        {
            var index = objects.Count - 1;
            var obj = objects[index];

            objects.RemoveAt(index);

            return (T)obj;
        }

        public void Add(object obj)
        {
            objects.Add(obj);
        }
    }
}
