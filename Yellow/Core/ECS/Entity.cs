using System;
using System.Collections.Generic;
using Yellow.Core.Components;

namespace Yellow.Core.ECS
{
    public class Entity
    {
        private readonly Dictionary<Type, Component> components;

        public World World { get; private set; }

        private TransformComponent transform = null;

        public TransformComponent Transform
        {
            get => transform;

            set
            {
                // removing transform
                if (value == null)
                {
                    components.Remove(typeof(TransformComponent));
                    World.RemoveComponent(transform);
                    transform = null;
                }
                else
                {
                    // if there was a transform already - removing it first
                    if (transform != null)
                    {
                        // removing from this.components not needed, as it will be simply overriden below
                        Transform = null;
                    }

                    transform = value;
                    value.owner = this;

                    components.Add(typeof(TransformComponent), value);
                    World.RegisterComponent(value);
                }
            }
        }

        private Graphic graphic = null;

        public Graphic Graphic
        {
            get => graphic;

            set
            {
                if (value == null)
                {
                    // removing graphic
                    components.Remove(typeof(Graphic));
                    World.RemoveComponent(graphic);
                    graphic = null;
                }
                else
                {
                    // if there was a graphic already - removing it first
                    if (graphic != null)
                    {
                        // removing from this.components not needed, as it will be simply overriden below
                        Graphic = null;
                    }

                    graphic = value;
                    value.owner = this;

                    components.Add(typeof(Graphic), value);
                    World.RegisterComponent(value);
                }
            }
        }

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

        public bool TryGet<T>(out T component) where T : Component
        {
            if (components.ContainsKey(typeof(T)))
            {
                component = (T)components[typeof(T)];

                return true;
            }

            component = null;

            return false;
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

        public bool AddChild(Entity entity)
        {
            if (transform == null || entity.transform == null)
            {
                return false;
            }

            transform.AddChild(entity.transform);
            
            return true;
        }

        public bool RemoveChild(Entity entity)
        {
            if (transform == null || entity.transform == null)
            {
                return false;
            }

            return transform.RemoveChild(entity.transform);
        }

        public static implicit operator TransformComponent(Entity entity)
        {
            return entity.transform;
        }
    }
}
