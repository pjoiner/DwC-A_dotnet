using DwC_A.Meta;
using DwC_A.Terms;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Tests.Models;

namespace Tests
{
    public class ArchiveWriterFixture
    {
        
        public ArchiveWriterFixture()
        {

        }

        public async Task<IEnumerable<Occurrence>> GetOccurrencesAsync()
        {
            using (var fs = File.OpenRead("./resources/occurrenceData.json"))
            {
                return await JsonSerializer.DeserializeAsync<Occurrence[]>(fs);
            }
        }

        public async Task<IEnumerable<Multimedia>> GetMultimediaAsync()
        {
            using (var fs = File.OpenRead("./resources/multimedia.json"))
            {
                return await JsonSerializer.DeserializeAsync<Multimedia[]>(fs);
            }
        }

        public FieldsMetaDataBuilder OccurrenceFieldsMetaDataBuilder
        {
            get
            {
                return FieldsMetaDataBuilder.Fields()
                    .AutomaticallyIndex()
                    .AddField(_ => _.Term(Terms.occurrenceID))
                    .AddField(_ => _.Term(Terms.basisOfRecord))
                    .AddField(_ => _.Term(Terms.scientificName))
                    .AddField(_ => _.Term(Terms.eventDate))
                    .AddField(_ => _.Term(Terms.decimalLatitude))
                    .AddField(_ => _.Term(Terms.decimalLongitude))
                    .AddField(_ => _.Term(Terms.geodeticDatum));
            }
        }

        public FieldsMetaDataBuilder MultimediaMetaDataBuilder
        {
            get
            {
                return FieldsMetaDataBuilder.Fields()
                    .AutomaticallyIndex()
                    .AddField(_ => _.Term(Terms.occurrenceID))
                    .AddField(_ => _.Term(Terms.type))
                    .AddField(_ => _.Term("http://purl.org/dc/terms/format"))
                    .AddField(_ => _.Term("http://purl.org/dc/terms/identifier"));
            }
        }
    }
}
