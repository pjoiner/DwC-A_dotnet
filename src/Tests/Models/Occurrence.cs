using System;

namespace Tests.Models
{
    public class Occurrence
    {
        public Guid OccurrenceID { get; set; }
        public string BasisOfRecord { get; set; }
        public string ScientificName { get; set; }
        public DateTime EventDate { get; set; }
        public double DecimalLatitude { get; set; }
        public double DecimalLongitude { get; set; }
        public string GeodeticDatum { get; set; }
    }

}
