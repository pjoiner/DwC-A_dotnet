using Microsoft.Extensions.Logging;

namespace DWC_A.Factories
{
    public class DefaultFactory : AbstractFactory
    {
        public DefaultFactory(ILoggerFactory loggerFactory = null)
            : base(loggerFactory)
        {

        }
    }
}
