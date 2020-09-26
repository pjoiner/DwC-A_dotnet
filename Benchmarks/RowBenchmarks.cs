using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using DwC_A;
using DwC_A.Factories;
using DwC_A.Meta;
using DwC_A.Terms;
using System;
using System.Linq;

namespace Benchmarks
{
    [Config(typeof(Config))]
    public class RowBenchmarks
    {
        private readonly string[] fields = { "id", "taxonID", "acceptedNameUsageID", "parentNameUsageID", "nameAccordingToID", "scientificName", "acceptedNameUsage", "parentNameUsage", "nameAccordingTo", "higherClassification", "class", "order", "family", "genus", "subgenus", "specificEpithet", "infraspecificEpithet", "taxonRank", "scientificNameAuthorship", "taxonomicStatus", "modified", "license", "bibliographicCitation", "references" };
        private IFileMetaData fileMetaData;
        private IRowFactory rowFactory;
        private int[] sequence;
        private readonly Consumer consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            Random rand = new Random();
            sequence = Enumerable.Range(0, fields.Length-1)
                .OrderBy(n => rand.Next())
                .ToArray();

            var metaData = FieldsMetaDataBuilder.Fields()
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
            var coreFileType = new CoreFileType();
            foreach(var m in metaData)
            {
                coreFileType.Field.Add(m);
            }
            var defaultFactory = new DefaultFactory();
            fileMetaData = defaultFactory.CreateCoreMetaData(coreFileType);
            
            rowFactory = defaultFactory.CreateRowFactory();
        }

        [Benchmark]
        public void DefaultAscending()
        {
            var row = rowFactory.CreateRow(fields, fileMetaData.Fields);
            Ascending(row);
        }

        [Benchmark]
        public void DefaultDescending()
        {
            var row = rowFactory.CreateRow(fields, fileMetaData.Fields);
            Descending(row);
        }

        [Benchmark]
        public void DefaultRandomOrder()
        {
            var row = rowFactory.CreateRow(fields, fileMetaData.Fields);
            RandomOrder(row);
        }

        [Benchmark]
        public void DefaultSparse()
        {
            var row = rowFactory.CreateRow(fields, fileMetaData.Fields);
            Sparse(row);
        }

        private void Descending(IRow row)
        {
            row[Terms.references].Consume(consumer);
            row[Terms.bibliographicCitation].Consume(consumer);
            row[Terms.taxonID].Consume(consumer);
            row[Terms.acceptedNameUsageID].Consume(consumer);
            row[Terms.parentNameUsageID].Consume(consumer);
            row[Terms.nameAccordingToID].Consume(consumer);
            row[Terms.scientificName].Consume(consumer);
            row[Terms.acceptedNameUsage].Consume(consumer);
            row[Terms.parentNameUsage].Consume(consumer);
            row[Terms.nameAccordingTo].Consume(consumer);
            row[Terms.higherClassification].Consume(consumer);
            row[Terms.@class].Consume(consumer);
            row[Terms.order].Consume(consumer);
            row[Terms.family].Consume(consumer);
            row[Terms.genus].Consume(consumer);
            row[Terms.subgenus].Consume(consumer);
            row[Terms.specificEpithet].Consume(consumer);
            row[Terms.infraspecificEpithet].Consume(consumer);
            row[Terms.taxonRank].Consume(consumer);
            row[Terms.scientificNameAuthorship].Consume(consumer);
            row[Terms.taxonomicStatus].Consume(consumer);
            row[Terms.modified].Consume(consumer);
            row[Terms.license].Consume(consumer);
        }

        private void Ascending(IRow row)
        {
            row[Terms.license].Consume(consumer);
            row[Terms.modified].Consume(consumer);
            row[Terms.taxonomicStatus].Consume(consumer);
            row[Terms.scientificNameAuthorship].Consume(consumer);
            row[Terms.taxonRank].Consume(consumer);
            row[Terms.infraspecificEpithet].Consume(consumer);
            row[Terms.specificEpithet].Consume(consumer);
            row[Terms.subgenus].Consume(consumer);
            row[Terms.genus].Consume(consumer);
            row[Terms.family].Consume(consumer);
            row[Terms.order].Consume(consumer);
            row[Terms.@class].Consume(consumer);
            row[Terms.higherClassification].Consume(consumer);
            row[Terms.nameAccordingTo].Consume(consumer);
            row[Terms.parentNameUsage].Consume(consumer);
            row[Terms.acceptedNameUsage].Consume(consumer);
            row[Terms.scientificName].Consume(consumer);
            row[Terms.nameAccordingToID].Consume(consumer);
            row[Terms.parentNameUsageID].Consume(consumer);
            row[Terms.acceptedNameUsageID].Consume(consumer);
            row[Terms.taxonID].Consume(consumer);
            row[Terms.bibliographicCitation].Consume(consumer);
            row[Terms.references].Consume(consumer);
        }

        private void RandomOrder(IRow row)
        {

            foreach(var idx in sequence)
            {
                row[idx].Consume(consumer);
            }
        }

        private void Sparse(IRow row)
        {
            row[Terms.license].Consume(consumer);
            row[Terms.references].Consume(consumer);
        }
    }
}
