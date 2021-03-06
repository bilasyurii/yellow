﻿using System;
using System.Collections.Generic;
using Yellow.Core.Components;

namespace Yellow.Core.ECS
{
    public class Entity
    {
        private readonly Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();

        public World world;

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
                    world.RemoveComponent(transform);
                    transform = null;
                }
                else
                {
                    // if there was a transform already - removing it first
                    if (transform != null)
                    {
                        Transform = null;
                    }

                    transform = value;
                    value.Owner = this;

                    components.Add(typeof(TransformComponent), value);
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
                    world.RemoveComponent(graphic);
                    graphic = null;
                }
                else
                {
                    // if there was a graphic already - removing it first
                    if (graphic != null)
                    {
                        Graphic = null;
                    }

                    graphic = value;
                    value.Owner = this;

                    components.Add(typeof(Graphic), value);
                }
            }
        }

        public Entity() { }

        public Entity(World world)
        {
            this.world = world;;
        }

        public bool Has(Type componentType)
        {
            return components.ContainsKey(componentType);
        }

        public bool Has<T>()
        {
            return components.ContainsKey(typeof(T));
        }

        public T Get<T>() where T : IComponent
        {
            return (T) components[typeof (T)];
        }

        public bool TryGet<T>(out T component) where T : class, IComponent
        {
            if (components.ContainsKey(typeof(T)))
            {
                component = (T)components[typeof(T)];

                return true;
            }

            component = null;

            return false;
        }

        public void Add<T>(T component) where T : IComponent
        {
            component.Owner = this;

            components.Add(typeof(T), component);
        }

        public void Add(IComponent component)
        {
            component.Owner = this;

            components.Add(component.GetType(), component);
        }

        public void Remove<T>() where T : IComponent
        {
            var type = typeof(T);
            var component = components[type];

            component.Owner = null;

            components.Remove(type);
            world.RemoveComponent(component);
        }

        public void Remove(IComponent component)
        {
            component.Owner = null;

            components.Remove(component.GetType());
            world.RemoveComponent(component);
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
