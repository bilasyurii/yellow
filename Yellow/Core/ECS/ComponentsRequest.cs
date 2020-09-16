using System;

namespace Yellow.Core.ECS
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ComponentsRequest : Attribute
    {
        public Type componentsType;

        public ComponentsRequest(Type componentsType)
        {
            this.componentsType = componentsType;
        }
    }
}
