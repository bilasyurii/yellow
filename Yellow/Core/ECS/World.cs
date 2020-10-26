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

        private readonly Dictionary<Type, List<Component>> components = new Dictionary<Type, List<Component>>();

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

            // injecting lists of components, that are requested
            // by system via marking properties with ComponentsRequest attribute
            var attributeType = typeof(ComponentsRequest);
            var properties = system.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.IsDefined(attributeType, false))
                {
                    var attributes = property.GetCustomAttributes(attributeType, false);

                    if (attributes.Length != 0)
                    {
                        var componentsType = ((ComponentsRequest)attributes[0]).componentsType;

                        if (!components.TryGetValue(componentsType, out var list))
                        {
                            list = new List<Component>();
                            components[componentsType] = list;
                        }

                        property.SetValue(system, list);

                        continue;
                    }
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
            List<Component> list;

            if (!componentsPool.TryGetValue(type, out var pool))
            {
                pool = new Pool<T>();

                if (!components.TryGetValue(type, out list))
                {
                    list = new List<Component>();
                    components.Add(type, list);
                }

                componentsPool.Add(type, pool);
            }
            else
            {
                list = components[type];
            }

            var component = (T)pool.Get();

            list.Add(component);

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
