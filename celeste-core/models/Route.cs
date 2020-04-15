
using System.Collections.Generic;
using System;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Celeste.Models
{
    [System.Serializable]
    public class Route
    {
        public List<RouteSystem> Systems { get; set; } = new List<RouteSystem>();

        public int Id { get; set; } = -1;
    }


    [System.Serializable]
    public class RouteSystem
    {
        [JsonProperty("bodies")]
        public List<Body> Bodies { get; set; } = new List<Body>();

        [JsonProperty("jumps")]
        public long Jumps { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }
    }

    [System.Serializable]
    public class Body
    {
        [JsonProperty("distance_to_arrival")]
        public long DistanceToArrival { get; set; }

        [JsonProperty("estimated_mapping_value")]
        public long EstimatedMappingValue { get; set; }

        [JsonProperty("estimated_scan_value")]
        public long EstimatedScanValue { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id64")]
        public string Id64 { get; set; }

        [JsonProperty("is_terraformable")]
        public bool IsTerraformable { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Subtype { AmmoniaWorld, EarthLikeWorld, HighMetalContentWorld, WaterWorld };

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TypeEnum { Planet };

    public partial class RoadToRichesRouteResponse
    {
        public static RoadToRichesRouteResponse FromJson(string json) => JsonConvert.DeserializeObject<RoadToRichesRouteResponse>(json, Celeste.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this RoadToRichesRouteResponse self) => JsonConvert.SerializeObject(self, Celeste.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                SubtypeConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class SubtypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Subtype) || t == typeof(Subtype?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Ammonia world":
                    return Subtype.AmmoniaWorld;
                case "Earth-like world":
                    return Subtype.EarthLikeWorld;
                case "High metal content world":
                    return Subtype.HighMetalContentWorld;
                case "Water world":
                    return Subtype.WaterWorld;
            }
            throw new Exception("Cannot unmarshal type Subtype");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Subtype)untypedValue;
            switch (value)
            {
                case Subtype.AmmoniaWorld:
                    serializer.Serialize(writer, "Ammonia world");
                    return;
                case Subtype.EarthLikeWorld:
                    serializer.Serialize(writer, "Earth-like world");
                    return;
                case Subtype.HighMetalContentWorld:
                    serializer.Serialize(writer, "High metal content world");
                    return;
                case Subtype.WaterWorld:
                    serializer.Serialize(writer, "Water world");
                    return;
            }
            throw new Exception("Cannot marshal type Subtype");
        }

        public static readonly SubtypeConverter Singleton = new SubtypeConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Planet")
            {
                return TypeEnum.Planet;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.Planet)
            {
                serializer.Serialize(writer, "Planet");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}
