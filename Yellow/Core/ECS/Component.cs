namespace Yellow.Core.ECS
{
    public abstract class Component
    {
        public World world;

        public Entity owner;
    }
}
