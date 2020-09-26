using BenchmarkDotNet.Attributes;
using DwC_A.Factories;
using DwC_A.Meta;
using DwC_A.Terms;

namespace Benchmarks
{
    [Config(typeof(Config))]
    public class MetaFileDataBenchmarks
    {
        private IAbstractFactory defaultFactory;
        private CoreFileType coreFileType;
        private FieldType[] metaData;

        [GlobalSetup]
        public void Setup()
        {
            metaData = FieldsMetaDataBuilder.Fields()
                .AutomaticallyIndex()
                .AddField(_ => _.Term("id"))
                .AddField(_ => _.Term(Terms.taxonID))
                .AddField(_ => _.Term(Terms.acceptedNameUsageID))
                .AddField(_ => _.Term(Terms.parentNameUsageID))
                .AddField(_ => _.Term(Terms.nameAccordingToID))
                .AddField(_ => _.Term(Terms.scientificName))
                .AddField(_ => _.Term(Terms.acceptedNameUsage))
                .AddField(_ => _.Term(Terms.parentNameUsage))
                .AddField(_ => _.Term(Terms.nameAccordingTo))
                .AddField(_ => _.Term(Terms.higherClassification))
                .AddField(_ => _.Term(Terms.@class))
                .AddField(_ => _.Term(Terms.order))
                .AddField(_ => _.Term(Terms.family))
                .AddField(_ => _.Term(Terms.genus))
                .AddField(_ => _.Term(Terms.subgenus))
                .AddField(_ => _.Term(Terms.specificEpithet))
                .AddField(_ => _.Term(Terms.infraspecificEpithet))
                .AddField(_ => _.Term(Terms.taxonRank))
                .AddField(_ => _.Term(Terms.scientificNameAuthorship))
                .AddField(_ => _.Term(Terms.taxonomicStatus))
                .AddField(_ => _.Term(Terms.modified))
                .AddField(_ => _.Term(Terms.license))
                .AddField(_ => _.Term(Terms.bibliographicCitation))
                .AddField(_ => _.Term(Terms.references))
                .Build();
            coreFileType = new CoreFileType();
            foreach (var m in metaData)
            {
                coreFileType.Field.Add(m);
            }
            defaultFactory = new DefaultFactory();
        }

        [Benchmark]
        public void DefaultDescending()
        {
            var fileMetaData = defaultFactory.CreateCoreMetaData(coreFileType);
            Descending(fileMetaData.Fields);
        }

        [Benchmark]
        public void DefaultAscending()
        {
            var fileMetaData = defaultFactory.CreateCoreMetaData(coreFileType);
            Ascending(fileMetaData.Fields);
        }

        private void Descending(IFieldMetaData fieldsMetaData)
        {
            var references = fieldsMetaData[Terms.references];
            var bibliographicCitation = fieldsMetaData[Terms.bibliographicCitation];
            var taxonID = fieldsMetaData[Terms.taxonID];
            var acceptedNameUsageID = fieldsMetaData[Terms.acceptedNameUsageID];
            var parentNameUsageID = fieldsMetaData[Terms.parentNameUsageID];
            var nameAccordingToID = fieldsMetaData[Terms.nameAccordingToID];
            var scientificName = fieldsMetaData[Terms.scientificName];
            var acceptedNameUsage = fieldsMetaData[Terms.acceptedNameUsage];
            var parentNameUsage = fieldsMetaData[Terms.parentNameUsage];
            var nameAccordingTo = fieldsMetaData[Terms.nameAccordingTo];
            var higherClassification = fieldsMetaData[Terms.higherClassification];
            var @class = fieldsMetaData[Terms.@class];
            var order = fieldsMetaData[Terms.order];
            var family = fieldsMetaData[Terms.family];
            var genus = fieldsMetaData[Terms.genus];
            var subgenus = fieldsMetaData[Terms.subgenus];
            var specificEpithet = fieldsMetaData[Terms.specificEpithet];
            var infraspecificEpithet = fieldsMetaData[Terms.infraspecificEpithet];
            var taxonRank = fieldsMetaData[Terms.taxonRank];
            var scientificNameAuthorship = fieldsMetaData[Terms.scientificNameAuthorship];
            var taxonomicStatus = fieldsMetaData[Terms.taxonomicStatus];
            var modified = fieldsMetaData[Terms.modified];
            var license = fieldsMetaData[Terms.license];
        }

        private void Ascending(IFieldMetaData fieldsMetaData)
        {
            var license = fieldsMetaData[Terms.license];
            var modified = fieldsMetaData[Terms.modified];
            var taxonomicStatus = fieldsMetaData[Terms.taxonomicStatus];
            var scientificNameAuthorship = fieldsMetaData[Terms.scientificNameAuthorship];
            var taxonRank = fieldsMetaData[Terms.taxonRank];
            var infraspecificEpithet = fieldsMetaData[Terms.infraspecificEpithet];
            var specificEpithet = fieldsMetaData[Terms.specificEpithet];
            var subgenus = fieldsMetaData[Terms.subgenus];
            var genus = fieldsMetaData[Terms.genus];
            var family = fieldsMetaData[Terms.family];
            var order = fieldsMetaData[Terms.order];
            var @class = fieldsMetaData[Terms.@class];
            var higherClassification = fieldsMetaData[Terms.higherClassification];
            var nameAccordingTo = fieldsMetaData[Terms.nameAccordingTo];
            var parentNameUsage = fieldsMetaData[Terms.parentNameUsage];
            var acceptedNameUsage = fieldsMetaData[Terms.acceptedNameUsage];
            var scientificName = fieldsMetaData[Terms.scientificName];
            var nameAccordingToID = fieldsMetaData[Terms.nameAccordingToID];
            var parentNameUsageID = fieldsMetaData[Terms.parentNameUsageID];
            var acceptedNameUsageID = fieldsMetaData[Terms.acceptedNameUsageID];
            var taxonID = fieldsMetaData[Terms.taxonID];
            var bibliographicCitation = fieldsMetaData[Terms.bibliographicCitation];
            var references = fieldsMetaData[Terms.references];
        }
    }
}
