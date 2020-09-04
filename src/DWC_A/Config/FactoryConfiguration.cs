using System;
using System.Collections.Generic;

namespace DwC_A.Config
{
    public class FactoryConfiguration
    {
        private readonly IDictionary<Type, object> configurationDictionary = new Dictionary<Type, object>();

        public void Add<T>(Action<T> configOptions) where T : new()
        {
            if(configOptions != null)
            {
                var config = new T();
                configOptions(config);
                configurationDictionary.Add(typeof(T), config);
            }
        }

        internal T GetOptions<T>() where T : class, new()
        {
            if(configurationDictionary.ContainsKey(typeof(T)))
            {
                return configurationDictionary[typeof(T)] as T;
            }
            return new T(); 
        }
    }
}
