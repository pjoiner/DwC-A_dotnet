using DwC_A.Config;
using System;

namespace DwC_A.Factories
{
    public class DefaultFactory : AbstractFactory
    {
        public DefaultFactory()
            :base(null)
        {

        }

        public DefaultFactory(Action<FactoryConfiguration> configFunc = null)
            :base(configFunc)
        {
        }
    }
}
