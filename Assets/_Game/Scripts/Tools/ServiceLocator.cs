using System;
using System.Collections.Generic;

namespace _Game.Scripts.Tools
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();

        public static void Init()
        {
            Instances.Clear();
        }
    
        public static void Register<TInstance>(TInstance instance)
        {
            Instances.TryAdd(typeof(TInstance), instance);
        }

        public static TInstance GetInstance<TInstance>()
             => (TInstance)Instances[typeof(TInstance)];
    }
}