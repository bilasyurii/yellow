namespace Yellow.Core.ECS
{
    public abstract class BaseComponent : IComponent
    {
        public Entity Owner { get; set; }
    }
}
