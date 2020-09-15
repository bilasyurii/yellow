namespace Yellow.Core.ECS
{
    public class System
    {
        public World World { private get; set; }

        public virtual void Update() { }
    }
}
