namespace Microsoft.Extensions.Logging.Abstractions
{
    //This implementation is necessary until the dotnet core team has released 
    //a newer version of Microsoft.Extensions.Logging.Abstractions that has
    //NullLoggerFactory under the Microsoft.Extensions.Logging.Abstractions
    //namespace
    internal class NullLoggerFactory : ILoggerFactory
    {
        public static readonly NullLoggerFactory Instance = new NullLoggerFactory();

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return NullLogger.Instance;
        }

        public void Dispose()
        {
        }
    }
}
