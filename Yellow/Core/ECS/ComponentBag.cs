using System;

namespace Yellow.Core.ECS
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ComponentBag : Attribute
    {
        public Type componentsType;

        public ComponentBag(Type componentsType)
        {
            this.componentsType = componentsType;
        }
    }
}
