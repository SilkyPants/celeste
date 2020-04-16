
using System.Globalization;
using System.IO;
using System.Linq;
using Celeste.Models;
using Celeste.Models.Spansh;
using CsvHelper;

namespace Celeste.Services.Spanch {
    public static class SpanchExtensions {
        public static Route R2RCsvToRoute(this RoutePlanningService planningService, string csvData) {
            
            var buffer = System.Text.Encoding.UTF8.GetBytes(csvData);
            using (var ms = new MemoryStream(buffer))
            using (var reader = new StreamReader(ms))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) 
            {
                var stops = csv.GetRecords<R2RRouteStopCsv>();
                var systems = stops.GroupBy( s => s.SystemName).Select(s => {
                    // TODO: Need to look up body and system IDs
                    return new StarSystem {
                        Name = s.Key,
                        Jumps = s.First()?.Jumps ?? 0,
                        Bodies = s.Select(b => {
                            return new Body {
                                Name = b.BodyName,
                                Subtype = b.BodySubtype,
                                IsTerraformable = b.IsTerraformable,
                                DistanceToArrival = b.DistanceToArrival,
                                EstimatedMappingValue = b.EstimatedMappingValue,
                                EstimatedScanValue = b.EstimatedScanValue,
                            };
                        }).AsEnumerable().ToList()
                    };
                });

                return new Route {
                    Systems = systems.ToList()
                };
            }
        }
    } 
}