using System;
using System.Collections.Generic;

namespace DwC_A.Config
{
    /// <summary>
    /// This class is used to store various configuration objects that can be used to 
    /// customize the behavior of the library.
    /// </summary>
    public class FactoryConfiguration
    {
        private readonly IDictionary<Type, object> configurationDictionary = new Dictionary<Type, object>();

        /// <summary>
        /// Adds a new configuration object
        /// </summary>
        /// <typeparam name="T">Type of the configuration object</typeparam>
        /// <param name="configOptions">A new configuration object is created and can be updated inside this lambda</param>
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
