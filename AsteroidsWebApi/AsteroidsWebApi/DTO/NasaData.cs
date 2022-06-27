using System.Text.Json.Serialization;

namespace AsteroidsWebApi.DTO
{
    public class NasaData
    {
        [JsonPropertyName("links")]
        public Links Links { get; set; }
        [JsonPropertyName("element_count")]
        public int ElementCount { get; set; }
        [JsonPropertyName("near_earth_objects")]
        public Dictionary<string, List<NearEarthObjects>> NearEarthObjects { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("next")]
        public string Next { get; set; }
        [JsonPropertyName("prev")]
        public string Prev { get; set; }
        [JsonPropertyName("self")]
        public string Self { get; set; }

    }

    public class NearEarthObjects
    {
        [JsonPropertyName("links")]
        public Links Links { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("neo_reference_id")]
        public string NeoRefId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("nasa_jpl_url")]
        public string NasaJplUrl { get; set; }
        [JsonPropertyName("absolute_magnitude_h")]
        public double AbsMagnitudeH { get; set; }
        [JsonPropertyName("estimated_diameter")]
        public Dictionary<string, MinMax>? AvgDiameter { get; set; }
        [JsonPropertyName("is_potentially_hazardous_asteroid")]
        public bool IsHazardousAsteroid { get; set; }
        [JsonPropertyName("close_approach_data")]
        public IList<CloseApproachData> CloseApproachData { get; set; }
        [JsonPropertyName("is_sentry_object")]
        public bool IsSentryObject { get; set; }
    }

    public class MinMax
    {
        [JsonPropertyName("estimated_diameter_min")]
        public double MinDiameter { get; set; }
        [JsonPropertyName("estimated_diameter_max")]
        public double MaxDiameter { get; set; }
    }

    public class CloseApproachData
    {
        [JsonPropertyName("close_approach_date")]
        public string ApproachDate { get; set; }
        [JsonPropertyName("close_approach_date_full")]
        public string ApproachDateFull { get; set; }
        [JsonPropertyName("epoch_date_close_approach")]
        public long EpochApproachDate { get; set; }
        [JsonPropertyName("relative_velocity")]
        public Velocity RelativeVelocity { get; set; }
        [JsonPropertyName("miss_distance")]
        public Distance MissDistance { get; set; }
        [JsonPropertyName("orbiting_body")]
        public string OrbitingBody { get; set; }
    }

    public class Velocity
    {
        [JsonPropertyName("kilometers_per_second")]
        public string KilometersSecond { get; set; }
        [JsonPropertyName("kilometers_per_hour")]
        public string KilometersHour { get; set; }
        [JsonPropertyName("miles_per_hour")]
        public string MilesHour { get; set; }
    }

    public class Distance
    {
        [JsonPropertyName("astronomical")]
        public string Astronomical { get; set; }
        [JsonPropertyName("lunar")]
        public string Lunar { get; set; }
        [JsonPropertyName("kilometers")]
        public string Kilometers { get; set; }
        [JsonPropertyName("miles")]
        public string Miles { get; set; }
    }
}