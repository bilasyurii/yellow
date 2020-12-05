using System;
using System.Collections.Generic;
using Yellow.Core.Boot;
using Yellow.Core.Components;
using Yellow.Core.Utils;

namespace Yellow.Core.ECS
{
    public class World
    {
        private readonly List<System> systems = new List<System>();

        private readonly Dictionary<Type, IComponentBag> components = new Dictionary<Type, IComponentBag>();

        private readonly Dictionary<Type, IPool> componentsPool = new Dictionary<Type, IPool>();

        private readonly Pool<Entity> entities;

        public readonly Entity root;

        public World(WorldBuilder worldBuilder)
        {
            var poolInitialSize = worldBuilder.entitiesPoolSize;

            entities = new Pool<Entity>(poolInitialSize);

            PrepopulatePool(poolInitialSize);

            root = CreateEntity();
            root.Transform = CreateComponent<TransformComponent>();
        }

        public void AddSystem(System system)
        {
            systems.Add(system);

            system.World = this;

            // injecting component bags
            var bagType = typeof(ComponentBag<>);
            var properties = system.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;

                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == bagType)
                {
                    var componentsType = propertyType.GetGenericArguments()[0];

                    if (!components.TryGetValue(componentsType, out var bag))
                    {
                        var constructedType = bagType.MakeGenericType(new[] { componentsType });
                        bag = (IComponentBag)Activator.CreateInstance(constructedType);
                        components[componentsType] = bag;
                    }

                    property.SetValue(system, bag);
                }
            }
        }

        public void RemoveSystem(System system)
        {
            systems.Remove(system);
        }

        public void Prepare()
        {
            foreach (var system in systems)
            {
                system.Prepare();
            }
        }

        public void FixedUpdate()
        {
        }

        public void Update()
        {
            foreach (var system in systems)
            {
                system.Update();
            }
        }

        public Entity CreateEntity()
        {
            var entity = entities.Get();

            entity.world = this;

            return entity;
        }

        public bool Add(Entity entity)
        {
            return root.AddChild(entity);
        }

        public T CreateComponent<T>() where T : Component, new()
        {
            var type = typeof(T);
            IComponentBag bag;

            if (!componentsPool.TryGetValue(type, out var pool))
            {
                pool = new Pool<T>();

                if (!components.TryGetValue(type, out bag))
                {
                    bag = new ComponentBag<T>();
                    components.Add(type, bag);
                }

                componentsPool.Add(type, pool);
            }
            else
            {
                bag = components[type];
            }

            var component = (T)pool.Get();

            bag.Add(component);

            return component;
        }

        public void RemoveComponent<T>(T component) where T : Component
        {
            var type = typeof(T);

            components[type].Remove(component);
            componentsPool[type].Add(component);
        }

        private void PrepopulatePool(int amount)
        {
            entities.Populate(amount);
        }
    }
}
