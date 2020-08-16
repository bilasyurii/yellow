using System;
using System.Collections.Generic;

namespace Yellow.Core.ECS
{
    public class Entity
    {
        private readonly Dictionary<Type, IComponent> components;

        public World World { get; private set; }

        public Entity(World world)
        {
            World = world;
            components = new Dictionary<Type, IComponent>();
        }

        public bool Has(Type componentType)
        {
            return components.ContainsKey(componentType);
        }

        public T Get<T>() where T : IComponent
        {
            return (T) components[typeof (T)];
        }

        public void Add(IComponent component)
        {
            components.Add(component.GetType(), component);
        }

        public void Remove<T>() where T : IComponent
        {
            components.Remove(typeof(T));
        }
    }
}
