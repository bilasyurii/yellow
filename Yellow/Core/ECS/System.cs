namespace Yellow.Core.ECS
{
    public class System
    {
        public World World { protected get; set; }

        public virtual void Update() { }
    }
}
