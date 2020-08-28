using System;
using System.Collections.Generic;
using Yellow.Assets;
using Yellow.Assets.Abstractions;

namespace Yellow.Core
{
    public static class Locator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        private static Game game = null;

        public static Game Game
        {
            get
            {
                return game ??= (Game)services[typeof(Game)];
            }
        }

        public static TService Get<TService>()
        {
            return (TService)services[typeof(TService)];
        }

        public static void Provide<TService>(TService service)
        {
            services.Add(typeof(TService), service);
        }

        public static void ProvideStandardServices()
        {
            Provide<IAssetManager>(new AssetManager());
        }
    }
}
