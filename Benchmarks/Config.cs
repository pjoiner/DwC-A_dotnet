using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace Benchmarks
{
    public class Config : ManualConfig
    {
        public Config()
        {
            var baseJob = Job.MediumRun;

            //AddJob(baseJob.WithNuGet("DwC-A_dotnet", "0.4.0").WithId("0.4.0"));
            //AddJob(baseJob.WithNuGet("DwC-A_dotnet", "0.5.0").WithId("0.5.0"));
            AddJob(baseJob.WithNuGet("DwC-A_dotnet", "0.5.2").WithId("0.5.2"));
        }
    }
}
