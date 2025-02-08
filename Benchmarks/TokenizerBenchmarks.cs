using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using DwC_A;
using DwC_A.Factories;
using DwC_A.Meta;

namespace Benchmarks
{
    [Config(typeof(Config))]
    public class TokenizerBenchmarks
    {
        private readonly string line = "id	taxonID	acceptedNameUsageID	parentNameUsageID	nameAccordingToID	scientificName	acceptedNameUsage	parentNameUsage	nameAccordingTo	higherClassification	class	order	family	genus	subgenus	specificEpithet	infraspecificEpithet	taxonRank	scientificNameAuthorship	taxonomicStatus	modified	license	bibliographicCitation	references";
#pragma warning disable CA1859 // We are measuring performance here
        private readonly IAbstractFactory abstractFactory = new DefaultFactory();
#pragma warning restore CA1859 // We are measuring performance here
        private ITokenizer tokenizer;
        private readonly Consumer consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            IFileMetaData fileMetaData = abstractFactory.CreateCoreMetaData(new CoreFileType()
            {
                FieldsTerminatedBy = "\\t",
                FieldsEnclosedBy = ""
            });
            tokenizer = abstractFactory.CreateTokenizer(fileMetaData);
        }

        [Benchmark]
        public void TokenizeString()
        {
            tokenizer.Split(line).Consume(consumer);
        }
    }
}
