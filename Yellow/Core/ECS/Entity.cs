using System;
using System.Collections.Generic;
using Yellow.Core.Components;

namespace Yellow.Core.ECS
{
    public class Entity
    {
        private readonly Dictionary<Type, Component> components;

        public World World { get; private set; }

        public TransformComponent Transform { get; private set; } = null;

        public Entity(World world)
        {
            World = world;
            components = new Dictionary<Type, Component>();
        }

        public bool Has(Type componentType)
        {
            return components.ContainsKey(componentType);
        }

        public bool Has<T>()
        {
            return components.ContainsKey(typeof(T));
        }

        public T Get<T>() where T : Component
        {
            return (T) components[typeof (T)];
        }

        public void Add<T>(T component) where T : Component
        {
            component.owner = this;

            components.Add(typeof(T), component);
            World.RegisterComponent(component);
        }

        public void Add(Component component)
        {
            component.owner = this;

            components.Add(component.GetType(), component);
            World.RegisterComponent(component);
        }

        public void Remove<T>() where T : Component
        {
            var type = typeof(T);
            var component = components[type];

            component.owner = null;

            components.Remove(type);
            World.RemoveComponent(component);
        }

        public void Remove(Component component)
        {
            component.owner = null;

            components.Remove(component.GetType());
            World.RemoveComponent(component);
        }
    }
}
