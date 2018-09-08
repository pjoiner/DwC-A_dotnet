using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Moq;

namespace Tests
{
    public static class TestLogger
    {
        private static readonly Mock<ILoggerFactory> loggerFactoryMock = new Mock<ILoggerFactory>();

        static TestLogger()
        {
            loggerFactoryMock.Setup(n => n.CreateLogger(It.IsAny<string>()))
                .Returns(TestLogger.DebugLogger);
        }

        public static ILoggerFactory LoggerFactory { get { return loggerFactoryMock.Object; } }

        public static ILogger DebugLogger { get; } = new DebugLogger("DebugLogger");
    }
}
