using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace Benchmarks
{
    public class Config : ManualConfig
    {
        public Config()
        {
            var baseJob = Job.ShortRun;
#if LOCALBUILD
            var package_0_8_0 = baseJob.WithId("0.8.0");
            AddJob(package_0_8_0.WithRuntime(ClrRuntime.Net48));
            AddJob(package_0_8_0.WithRuntime(CoreRuntime.Core50));
            AddJob(package_0_8_0.WithRuntime(CoreRuntime.Core60));
            AddJob(package_0_8_0.WithRuntime(CoreRuntime.Core70));
            //AddJob(package_0_8_0.WithRuntime(CoreRuntime.Core80));
#else
            var package_0_5_2 = baseJob.WithNuGet("DwC-A_dotnet", "0.5.2").WithId("0.5.2");
            var package_0_6_2 = baseJob.WithNuGet("DwC-A_dotnet", "0.6.2").WithId("0.6.2");
            var package_0_7_0 = baseJob.WithNuGet("DwC-A_dotnet", "0.7.0").WithId("0.7.0");

            AddJob(package_0_5_2.WithRuntime(ClrRuntime.Net48));
            AddJob(package_0_5_2.WithRuntime(CoreRuntime.Core50));
            AddJob(package_0_5_2.WithRuntime(CoreRuntime.Core60));
            AddJob(package_0_5_2.WithRuntime(CoreRuntime.Core70));
            //AddJob(package_0_5_2.WithRuntime(CoreRuntime.Core80));
            AddJob(package_0_6_2.WithRuntime(ClrRuntime.Net48));
            AddJob(package_0_6_2.WithRuntime(CoreRuntime.Core50));
            AddJob(package_0_6_2.WithRuntime(CoreRuntime.Core60));
            AddJob(package_0_6_2.WithRuntime(CoreRuntime.Core70));
            //AddJob(package_0_6_2.WithRuntime(CoreRuntime.Core80));
            AddJob(package_0_7_0.WithRuntime(ClrRuntime.Net48));
            AddJob(package_0_7_0.WithRuntime(CoreRuntime.Core50));
            AddJob(package_0_7_0.WithRuntime(CoreRuntime.Core60));
            AddJob(package_0_7_0.WithRuntime(CoreRuntime.Core70));
            //AddJob(package_0_7_0.WithRuntime(CoreRuntime.Core80));
#endif
        }
    }
}
