namespace Yellow.Core.ECS
{
    public interface IComponent
    {
        Entity Owner { get; set; }
    }
}
