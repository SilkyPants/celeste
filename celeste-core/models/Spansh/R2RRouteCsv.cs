
using CsvHelper.Configuration.Attributes;

namespace Celeste.Models.Spansh
{
    public class R2RRouteStopCsv
    {
        [Name("System Name")]
        public string SystemName { get; set; }

        [Name("Body Name")]
        public string BodyName { get; set; }

        [Name("Body Subtype")]
        public string BodySubtype { get; set; }

        [Name("Is Terraformable")]
        [BooleanFalseValues("No")]
        [BooleanTrueValues("Yes")]
        public bool IsTerraformable { get; set; }

        [Name("Distance To Arrival")]
        public long DistanceToArrival { get; set; }

        [Name("Estimated Scan Value")]
        public long EstimatedScanValue { get; set; }

        [Name("Estimated Mapping Value")]
        public long EstimatedMappingValue { get; set; }

        [Name("Jumps")]
        public long Jumps { get; set; }
    }
}