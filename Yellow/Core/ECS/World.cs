using System;
using System.Collections.Generic;
using Yellow.Core.Boot;
using Yellow.Core.Utils;

namespace Yellow.Core.ECS
{
    public class World
    {
        private readonly List<System> systems = new List<System>();

        private readonly Dictionary<Type, List<Component>> components = new Dictionary<Type, List<Component>>();

        private readonly Pool entities;

        public World(WorldBuilder worldBuilder)
        {
            var poolInitialSize = worldBuilder.entitiesPoolSize;

            entities = new Pool(poolInitialSize);

            PrepopulatePool(poolInitialSize);
        }

        private void PrepopulatePool(int size)
        {
            for (int i = 0; i < size; ++i)
            {
                entities.Add(new Entity(this));
            }
        }

        public void AddSystem(System system)
        {
            systems.Add(system);

            system.World = this;

            // injecting lists of components, that are requested
            // by system via marking properties with ComponentBag attribute
            var attributeType = typeof(ComponentBag);
            var properties = system.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.IsDefined(attributeType, false))
                {
                    var attributes = property.GetCustomAttributes(attributeType, false);

                    if (attributes.Length != 0)
                    {
                        var componentsType = ((ComponentBag)attributes[0]).componentsType;

                        if (!components.TryGetValue(componentsType, out var list))
                        {
                            list = new List<Component>();
                            components[componentsType] = list;
                        }

                        property.SetValue(system, list);
                    }
                }
            }
        }

        public void RemoveSystem(System system)
        {
            systems.Remove(system);
        }

        public void Tick()
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
            if (entities.IsEmpty)
            {
                return new Entity(this);
            }
            else
            {
                return (Entity)entities.Get();
            }
        }

        public void RegisterComponent<T>(T component) where T : Component
        {
            component.world = this;

            var type = typeof(T);

            if (components.TryGetValue(type, out var list))
            {
                list.Add(component);
            }
            else
            {
                components.Add(type, new List<Component>()
                {
                    component
                });
            }
        }

        public void RemoveComponent<T>(T component) where T : Component
        {
            component.world = null;

            components[typeof(T)].Remove(component);
        }
    }
}
