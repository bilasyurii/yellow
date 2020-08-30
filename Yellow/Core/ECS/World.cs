using System;
using System.Collections.Generic;
using Yellow.Core.Utils;

namespace Yellow.Core.ECS
{
    public class World
    {
        private readonly List<System> systems = new List<System>();

        private readonly Dictionary<Type, List<Component>> components = new Dictionary<Type, List<Component>>();

        private readonly Pool entities;

        public World(int poolInitialSize)
        {
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
        }

        public void RemoveSystem(System system)
        {
            systems.Remove(system);
        }

        public void Update(float deltaTime)
        {
            foreach (var system in systems)
            {
                system.Update(deltaTime);
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
