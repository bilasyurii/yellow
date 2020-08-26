using System;
using System.Collections.Generic;

namespace Yellow.Core
{
    public static class Locator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Provide<TService>(TService service)
        {
            services.Add(typeof(TService), service);
        }

        public static TService Get<TService>()
        {
            return (TService)services[typeof(TService)];
        }
    }
}
