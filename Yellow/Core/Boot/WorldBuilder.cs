namespace Yellow.Core.Boot
{
    public class WorldBuilder
    {
        public int entitiesPoolSize = 100;

        public WorldBuilder SetEntitiesPoolSize(int size)
        {
            entitiesPoolSize = size;

            return this;
        }
    }
}
