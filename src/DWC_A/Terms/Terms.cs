﻿using System.Text.RegularExpressions;

namespace DwC_A.Terms
{
    public static class Terms
    {
        public static readonly string acceptedNameUsage = "http://rs.tdwg.org/dwc/terms/acceptedNameUsage";
        public static readonly string acceptedNameUsageID = "http://rs.tdwg.org/dwc/terms/acceptedNameUsageID";
        public static readonly string accessRights = "http://purl.org/dc/terms/accessRights";
        public static readonly string associatedMedia = "http://rs.tdwg.org/dwc/terms/associatedMedia";
        public static readonly string associatedOccurrences = "http://rs.tdwg.org/dwc/terms/associatedOccurrences";
        public static readonly string associatedOrganisms = "http://rs.tdwg.org/dwc/terms/associatedOrganisms";
        public static readonly string associatedReferences = "http://rs.tdwg.org/dwc/terms/associatedReferences";
        public static readonly string associatedSequences = "http://rs.tdwg.org/dwc/terms/associatedSequences";
        public static readonly string associatedTaxa = "http://rs.tdwg.org/dwc/terms/associatedTaxa";
        public static readonly string basisOfRecord = "http://rs.tdwg.org/dwc/terms/basisOfRecord";
        public static readonly string bed = "http://rs.tdwg.org/dwc/terms/bed";
        public static readonly string behavior = "http://rs.tdwg.org/dwc/terms/behavior";
        public static readonly string bibliographicCitation = "http://purl.org/dc/terms/bibliographicCitation";
        public static readonly string catalogNumber = "http://rs.tdwg.org/dwc/terms/catalogNumber";
        public static readonly string @class = "http://rs.tdwg.org/dwc/terms/class";
        public static readonly string collectionCode = "http://rs.tdwg.org/dwc/terms/collectionCode";
        public static readonly string collectionID = "http://rs.tdwg.org/dwc/terms/collectionID";
        public static readonly string continent = "http://rs.tdwg.org/dwc/terms/continent";
        public static readonly string coordinatePrecision = "http://rs.tdwg.org/dwc/terms/coordinatePrecision";
        public static readonly string coordinateUncertaintyInMeters = "http://rs.tdwg.org/dwc/terms/coordinateUncertaintyInMeters";
        public static readonly string country = "http://rs.tdwg.org/dwc/terms/country";
        public static readonly string countryCode = "http://rs.tdwg.org/dwc/terms/countryCode";
        public static readonly string county = "http://rs.tdwg.org/dwc/terms/county";
        public static readonly string dataGeneralizations = "http://rs.tdwg.org/dwc/terms/dataGeneralizations";
        public static readonly string datasetID = "http://rs.tdwg.org/dwc/terms/datasetID";
        public static readonly string datasetName = "http://rs.tdwg.org/dwc/terms/datasetName";
        public static readonly string dateIdentified = "http://rs.tdwg.org/dwc/terms/dateIdentified";
        public static readonly string day = "http://rs.tdwg.org/dwc/terms/day";
        public static readonly string decimalLatitude = "http://rs.tdwg.org/dwc/terms/decimalLatitude";
        public static readonly string decimalLongitude = "http://rs.tdwg.org/dwc/terms/decimalLongitude";
        public static readonly string disposition = "http://rs.tdwg.org/dwc/terms/disposition";
        public static readonly string dynamicProperties = "http://rs.tdwg.org/dwc/terms/dynamicProperties";
        public static readonly string earliestAgeOrLowestStage = "http://rs.tdwg.org/dwc/terms/earliestAgeOrLowestStage";
        public static readonly string earliestEonOrLowestEonothem = "http://rs.tdwg.org/dwc/terms/earliestEonOrLowestEonothem";
        public static readonly string earliestEpochOrLowestSeries = "http://rs.tdwg.org/dwc/terms/earliestEpochOrLowestSeries";
        public static readonly string earliestEraOrLowestErathem = "http://rs.tdwg.org/dwc/terms/earliestEraOrLowestErathem";
        public static readonly string earliestGeochronologicalEra = "http://rs.tdwg.org/dwc/iri/earliestGeochronologicalEra";
        public static readonly string earliestPeriodOrLowestSystem = "http://rs.tdwg.org/dwc/terms/earliestPeriodOrLowestSystem";
        public static readonly string endDayOfYear = "http://rs.tdwg.org/dwc/terms/endDayOfYear";
        public static readonly string establishmentMeans = "http://rs.tdwg.org/dwc/terms/establishmentMeans";
        public static readonly string Event = "http://rs.tdwg.org/dwc/terms/Event";
        public static readonly string eventDate = "http://rs.tdwg.org/dwc/terms/eventDate";
        public static readonly string eventID = "http://rs.tdwg.org/dwc/terms/eventID";
        public static readonly string eventRemarks = "http://rs.tdwg.org/dwc/terms/eventRemarks";
        public static readonly string eventTime = "http://rs.tdwg.org/dwc/terms/eventTime";
        public static readonly string family = "http://rs.tdwg.org/dwc/terms/family";
        public static readonly string fieldNotes = "http://rs.tdwg.org/dwc/terms/fieldNotes";
        public static readonly string fieldNumber = "http://rs.tdwg.org/dwc/terms/fieldNumber";
        public static readonly string footprintSpatialFit = "http://rs.tdwg.org/dwc/terms/footprintSpatialFit";
        public static readonly string footprintSRS = "http://rs.tdwg.org/dwc/terms/footprintSRS";
        public static readonly string footprintWKT = "http://rs.tdwg.org/dwc/terms/footprintWKT";
        public static readonly string formation = "http://rs.tdwg.org/dwc/terms/formation";
        public static readonly string FossilSpecimen = "http://rs.tdwg.org/dwc/terms/FossilSpecimen";
        public static readonly string fromLithostratigraphicUnit = "http://rs.tdwg.org/dwc/iri/fromLithostratigraphicUnit";
        public static readonly string genus = "http://rs.tdwg.org/dwc/terms/genus";
        public static readonly string geodeticDatum = "http://rs.tdwg.org/dwc/terms/geodeticDatum";
        public static readonly string GeologicalContext = "http://rs.tdwg.org/dwc/terms/GeologicalContext";
        public static readonly string geologicalContextID = "http://rs.tdwg.org/dwc/terms/geologicalContextID";
        public static readonly string georeferencedBy = "http://rs.tdwg.org/dwc/terms/georeferencedBy";
        public static readonly string georeferencedDate = "http://rs.tdwg.org/dwc/terms/georeferencedDate";
        public static readonly string georeferenceProtocol = "http://rs.tdwg.org/dwc/terms/georeferenceProtocol";
        public static readonly string georeferenceRemarks = "http://rs.tdwg.org/dwc/terms/georeferenceRemarks";
        public static readonly string georeferenceSources = "http://rs.tdwg.org/dwc/terms/georeferenceSources";
        public static readonly string georeferenceVerificationStatus = "http://rs.tdwg.org/dwc/terms/georeferenceVerificationStatus";
        public static readonly string group = "http://rs.tdwg.org/dwc/terms/group";
        public static readonly string habitat = "http://rs.tdwg.org/dwc/terms/habitat";
        public static readonly string higherClassification = "http://rs.tdwg.org/dwc/terms/higherClassification";
        public static readonly string higherGeography = "http://rs.tdwg.org/dwc/terms/higherGeography";
        public static readonly string higherGeographyID = "http://rs.tdwg.org/dwc/terms/higherGeographyID";
        public static readonly string highestBiostratigraphicZone = "http://rs.tdwg.org/dwc/terms/highestBiostratigraphicZone";
        public static readonly string HumanObservation = "http://rs.tdwg.org/dwc/terms/HumanObservation";
        public static readonly string Identification = "http://rs.tdwg.org/dwc/terms/Identification";
        public static readonly string identificationID = "http://rs.tdwg.org/dwc/terms/identificationID";
        public static readonly string identificationQualifier = "http://rs.tdwg.org/dwc/terms/identificationQualifier";
        public static readonly string identificationReferences = "http://rs.tdwg.org/dwc/terms/identificationReferences";
        public static readonly string identificationRemarks = "http://rs.tdwg.org/dwc/terms/identificationRemarks";
        public static readonly string identificationVerificationStatus = "http://rs.tdwg.org/dwc/terms/identificationVerificationStatus";
        public static readonly string identifiedBy = "http://rs.tdwg.org/dwc/terms/identifiedBy";
        public static readonly string inCollection = "http://rs.tdwg.org/dwc/iri/inCollection";
        public static readonly string inDataset = "http://rs.tdwg.org/dwc/iri/inDataset";
        public static readonly string inDescribedPlace = "http://rs.tdwg.org/dwc/iri/inDescribedPlace";
        public static readonly string individualCount = "http://rs.tdwg.org/dwc/terms/individualCount";
        public static readonly string informationWithheld = "http://rs.tdwg.org/dwc/terms/informationWithheld";
        public static readonly string infraspecificEpithet = "http://rs.tdwg.org/dwc/terms/infraspecificEpithet";
        public static readonly string institutionCode = "http://rs.tdwg.org/dwc/terms/institutionCode";
        public static readonly string institutionID = "http://rs.tdwg.org/dwc/terms/institutionID";
        public static readonly string island = "http://rs.tdwg.org/dwc/terms/island";
        public static readonly string islandGroup = "http://rs.tdwg.org/dwc/terms/islandGroup";
        public static readonly string kingdom = "http://rs.tdwg.org/dwc/terms/kingdom";
        public static readonly string language = "http://purl.org/dc/terms/language";
        public static readonly string latestAgeOrHighestStage = "http://rs.tdwg.org/dwc/terms/latestAgeOrHighestStage";
        public static readonly string latestEonOrHighestEonothem = "http://rs.tdwg.org/dwc/terms/latestEonOrHighestEonothem";
        public static readonly string latestEpochOrHighestSeries = "http://rs.tdwg.org/dwc/terms/latestEpochOrHighestSeries";
        public static readonly string latestEraOrHighestErathem = "http://rs.tdwg.org/dwc/terms/latestEraOrHighestErathem";
        public static readonly string latestGeochronologicalEra = "http://rs.tdwg.org/dwc/iri/latestGeochronologicalEra";
        public static readonly string latestPeriodOrHighestSystem = "http://rs.tdwg.org/dwc/terms/latestPeriodOrHighestSystem";
        public static readonly string license = "http://purl.org/dc/terms/license";
        public static readonly string lifeStage = "http://rs.tdwg.org/dwc/terms/lifeStage";
        public static readonly string lithostratigraphicTerms = "http://rs.tdwg.org/dwc/terms/lithostratigraphicTerms";
        public static readonly string LivingSpecimen = "http://rs.tdwg.org/dwc/terms/LivingSpecimen";
        public static readonly string locality = "http://rs.tdwg.org/dwc/terms/locality";
        public static readonly string Location = "http://purl.org/dc/terms/Location";
        public static readonly string locationAccordingTo = "http://rs.tdwg.org/dwc/terms/locationAccordingTo";
        public static readonly string locationID = "http://rs.tdwg.org/dwc/terms/locationID";
        public static readonly string locationRemarks = "http://rs.tdwg.org/dwc/terms/locationRemarks";
        public static readonly string lowestBiostratigraphicZone = "http://rs.tdwg.org/dwc/terms/lowestBiostratigraphicZone";
        public static readonly string MachineObservation = "http://rs.tdwg.org/dwc/terms/MachineObservation";
        public static readonly string MaterialSample = "http://rs.tdwg.org/dwc/terms/MaterialSample";
        public static readonly string materialSampleID = "http://rs.tdwg.org/dwc/terms/materialSampleID";
        public static readonly string maximumDepthInMeters = "http://rs.tdwg.org/dwc/terms/maximumDepthInMeters";
        public static readonly string maximumDistanceAboveSurfaceInMeters = "http://rs.tdwg.org/dwc/terms/maximumDistanceAboveSurfaceInMeters";
        public static readonly string maximumElevationInMeters = "http://rs.tdwg.org/dwc/terms/maximumElevationInMeters";
        public static readonly string measurementAccuracy = "http://rs.tdwg.org/dwc/terms/measurementAccuracy";
        public static readonly string measurementDeterminedBy = "http://rs.tdwg.org/dwc/terms/measurementDeterminedBy";
        public static readonly string measurementDeterminedDate = "http://rs.tdwg.org/dwc/terms/measurementDeterminedDate";
        public static readonly string measurementID = "http://rs.tdwg.org/dwc/terms/measurementID";
        public static readonly string measurementMethod = "http://rs.tdwg.org/dwc/terms/measurementMethod";
        public static readonly string MeasurementOrFact = "http://rs.tdwg.org/dwc/terms/MeasurementOrFact";
        public static readonly string measurementRemarks = "http://rs.tdwg.org/dwc/terms/measurementRemarks";
        public static readonly string measurementType = "http://rs.tdwg.org/dwc/terms/measurementType";
        public static readonly string measurementUnit = "http://rs.tdwg.org/dwc/terms/measurementUnit";
        public static readonly string measurementValue = "http://rs.tdwg.org/dwc/terms/measurementValue";
        public static readonly string member = "http://rs.tdwg.org/dwc/terms/member";
        public static readonly string minimumDepthInMeters = "http://rs.tdwg.org/dwc/terms/minimumDepthInMeters";
        public static readonly string minimumDistanceAboveSurfaceInMeters = "http://rs.tdwg.org/dwc/terms/minimumDistanceAboveSurfaceInMeters";
        public static readonly string minimumElevationInMeters = "http://rs.tdwg.org/dwc/terms/minimumElevationInMeters";
        public static readonly string modified = "http://purl.org/dc/terms/modified";
        public static readonly string month = "http://rs.tdwg.org/dwc/terms/month";
        public static readonly string municipality = "http://rs.tdwg.org/dwc/terms/municipality";
        public static readonly string nameAccordingTo = "http://rs.tdwg.org/dwc/terms/nameAccordingTo";
        public static readonly string nameAccordingToID = "http://rs.tdwg.org/dwc/terms/nameAccordingToID";
        public static readonly string namePublishedIn = "http://rs.tdwg.org/dwc/terms/namePublishedIn";
        public static readonly string namePublishedInID = "http://rs.tdwg.org/dwc/terms/namePublishedInID";
        public static readonly string namePublishedInYear = "http://rs.tdwg.org/dwc/terms/namePublishedInYear";
        public static readonly string nomenclaturalCode = "http://rs.tdwg.org/dwc/terms/nomenclaturalCode";
        public static readonly string nomenclaturalStatus = "http://rs.tdwg.org/dwc/terms/nomenclaturalStatus";
        public static readonly string Occurrence = "http://rs.tdwg.org/dwc/terms/Occurrence";
        public static readonly string occurrenceID = "http://rs.tdwg.org/dwc/terms/occurrenceID";
        public static readonly string occurrenceRemarks = "http://rs.tdwg.org/dwc/terms/occurrenceRemarks";
        public static readonly string occurrenceStatus = "http://rs.tdwg.org/dwc/terms/occurrenceStatus";
        public static readonly string order = "http://rs.tdwg.org/dwc/terms/order";
        public static readonly string Organism = "http://rs.tdwg.org/dwc/terms/Organism";
        public static readonly string organismID = "http://rs.tdwg.org/dwc/terms/organismID";
        public static readonly string organismName = "http://rs.tdwg.org/dwc/terms/organismName";
        public static readonly string organismQuantity = "http://rs.tdwg.org/dwc/terms/organismQuantity";
        public static readonly string organismQuantityType = "http://rs.tdwg.org/dwc/terms/organismQuantityType";
        public static readonly string organismRemarks = "http://rs.tdwg.org/dwc/terms/organismRemarks";
        public static readonly string organismScope = "http://rs.tdwg.org/dwc/terms/organismScope";
        public static readonly string originalNameUsage = "http://rs.tdwg.org/dwc/terms/originalNameUsage";
        public static readonly string originalNameUsageID = "http://rs.tdwg.org/dwc/terms/originalNameUsageID";
        public static readonly string otherCatalogNumbers = "http://rs.tdwg.org/dwc/terms/otherCatalogNumbers";
        public static readonly string ownerInstitutionCode = "http://rs.tdwg.org/dwc/terms/ownerInstitutionCode";
        public static readonly string parentEventID = "http://rs.tdwg.org/dwc/terms/parentEventID";
        public static readonly string parentNameUsage = "http://rs.tdwg.org/dwc/terms/parentNameUsage";
        public static readonly string parentNameUsageID = "http://rs.tdwg.org/dwc/terms/parentNameUsageID";
        public static readonly string phylum = "http://rs.tdwg.org/dwc/terms/phylum";
        public static readonly string pointRadiusSpatialFit = "http://rs.tdwg.org/dwc/terms/pointRadiusSpatialFit";
        public static readonly string preparations = "http://rs.tdwg.org/dwc/terms/preparations";
        public static readonly string PreservedSpecimen = "http://rs.tdwg.org/dwc/terms/PreservedSpecimen";
        public static readonly string previousIdentifications = "http://rs.tdwg.org/dwc/terms/previousIdentifications";
        public static readonly string recordedBy = "http://rs.tdwg.org/dwc/terms/recordedBy";
        public static readonly string recordNumber = "http://rs.tdwg.org/dwc/terms/recordNumber";
        public static readonly string references = "http://purl.org/dc/terms/references";
        public static readonly string relatedResourceID = "http://rs.tdwg.org/dwc/terms/relatedResourceID";
        public static readonly string relationshipAccordingTo = "http://rs.tdwg.org/dwc/terms/relationshipAccordingTo";
        public static readonly string relationshipEstablishedDate = "http://rs.tdwg.org/dwc/terms/relationshipEstablishedDate";
        public static readonly string relationshipOfResource = "http://rs.tdwg.org/dwc/terms/relationshipOfResource";
        public static readonly string relationshipRemarks = "http://rs.tdwg.org/dwc/terms/relationshipRemarks";
        public static readonly string reproductiveCondition = "http://rs.tdwg.org/dwc/terms/reproductiveCondition";
        public static readonly string resourceID = "http://rs.tdwg.org/dwc/terms/resourceID";
        public static readonly string ResourceRelationship = "http://rs.tdwg.org/dwc/terms/ResourceRelationship";
        public static readonly string resourceRelationshipID = "http://rs.tdwg.org/dwc/terms/resourceRelationshipID";
        public static readonly string rightsHolder = "http://purl.org/dc/terms/rightsHolder";
        public static readonly string sampleSizeUnit = "http://rs.tdwg.org/dwc/terms/sampleSizeUnit";
        public static readonly string sampleSizeValue = "http://rs.tdwg.org/dwc/terms/sampleSizeValue";
        public static readonly string samplingEffort = "http://rs.tdwg.org/dwc/terms/samplingEffort";
        public static readonly string samplingProtocol = "http://rs.tdwg.org/dwc/terms/samplingProtocol";
        public static readonly string scientificName = "http://rs.tdwg.org/dwc/terms/scientificName";
        public static readonly string scientificNameAuthorship = "http://rs.tdwg.org/dwc/terms/scientificNameAuthorship";
        public static readonly string scientificNameID = "http://rs.tdwg.org/dwc/terms/scientificNameID";
        public static readonly string sex = "http://rs.tdwg.org/dwc/terms/sex";
        public static readonly string specificEpithet = "http://rs.tdwg.org/dwc/terms/specificEpithet";
        public static readonly string startDayOfYear = "http://rs.tdwg.org/dwc/terms/startDayOfYear";
        public static readonly string stateProvince = "http://rs.tdwg.org/dwc/terms/stateProvince";
        public static readonly string subgenus = "http://rs.tdwg.org/dwc/terms/subgenus";
        public static readonly string Taxon = "http://rs.tdwg.org/dwc/terms/Taxon";
        public static readonly string taxonConceptID = "http://rs.tdwg.org/dwc/terms/taxonConceptID";
        public static readonly string taxonID = "http://rs.tdwg.org/dwc/terms/taxonID";
        public static readonly string taxonomicStatus = "http://rs.tdwg.org/dwc/terms/taxonomicStatus";
        public static readonly string taxonRank = "http://rs.tdwg.org/dwc/terms/taxonRank";
        public static readonly string taxonRemarks = "http://rs.tdwg.org/dwc/terms/taxonRemarks";
        public static readonly string toTaxon = "http://rs.tdwg.org/dwc/iri/toTaxon";
        public static readonly string type = "http://purl.org/dc/terms/type";
        public static readonly string typeStatus = "http://rs.tdwg.org/dwc/terms/typeStatus";
        public static readonly string UseWithIRI = "http://rs.tdwg.org/dwc/terms/attributes/UseWithIRI";
        public static readonly string verbatimCoordinates = "http://rs.tdwg.org/dwc/terms/verbatimCoordinates";
        public static readonly string verbatimCoordinateSystem = "http://rs.tdwg.org/dwc/terms/verbatimCoordinateSystem";
        public static readonly string verbatimDepth = "http://rs.tdwg.org/dwc/terms/verbatimDepth";
        public static readonly string verbatimElevation = "http://rs.tdwg.org/dwc/terms/verbatimElevation";
        public static readonly string verbatimEventDate = "http://rs.tdwg.org/dwc/terms/verbatimEventDate";
        public static readonly string verbatimLatitude = "http://rs.tdwg.org/dwc/terms/verbatimLatitude";
        public static readonly string verbatimLocality = "http://rs.tdwg.org/dwc/terms/verbatimLocality";
        public static readonly string verbatimLongitude = "http://rs.tdwg.org/dwc/terms/verbatimLongitude";
        public static readonly string verbatimSRS = "http://rs.tdwg.org/dwc/terms/verbatimSRS";
        public static readonly string verbatimTaxonRank = "http://rs.tdwg.org/dwc/terms/verbatimTaxonRank";
        public static readonly string vernacularName = "http://rs.tdwg.org/dwc/terms/vernacularName";
        public static readonly string waterBody = "http://rs.tdwg.org/dwc/terms/waterBody";
        public static readonly string year = "http://rs.tdwg.org/dwc/terms/year";

        public static string ShortName(string term)
        {
            if(term == null)
            {
                return null;
            }
            Regex regex = new Regex("[^/]+$");
            var match = regex.Match(term);
            return string.IsNullOrEmpty(match.Value) ? term : match.Value;
        }
    }
}
