using System.Text.Json.Serialization;

namespace AsteroidsWebApi.DTO
{
    public class Asteroids
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("diameter")]
        public double Diameter { get; set; }
        [JsonPropertyName("close_approach_data")]
        public AsteroidApproachData ApproachData { get; set; }
        [JsonIgnore]
        public bool IsHazardousAsteroid { get; set; }
    }

    public class AsteroidApproachData
    {
        [JsonPropertyName("kilometers_per_hour")]
        public string KilometersHour { get; set; }
        [JsonPropertyName("close_approach_date")]
        public DateTime ApproachDate { get; set; }
        [JsonPropertyName("orbiting_body")]
        public string OrbitingBody { get; set; }
    }

}
