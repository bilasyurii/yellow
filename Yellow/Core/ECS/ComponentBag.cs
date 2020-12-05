using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yellow.Core.ECS
{
    public class ComponentBag<T> : IComponentBag, IEnumerable<T> where T: Component
    {
        private readonly List<T> components = new List<T>();

        public void Add(Component component)
        {
            components.Add((T)component);
        }

        public void Remove(Component component)
        {
            components.Remove((T)component);
        }

        public void Add(T component)
        {
            components.Add(component);
        }

        public void Remove(T component)
        {
            components.Remove(component);
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (components.Count == 0)
            {
                return Enumerable.Empty<T>().GetEnumerator();
            }

            return components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (components.Count == 0)
            {
                return Enumerable.Empty<T>().GetEnumerator();
            }

            return components.GetEnumerator();
        }
    }
}
