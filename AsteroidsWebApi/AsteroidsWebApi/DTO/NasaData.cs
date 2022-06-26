namespace AsteroidsWebApi.DTO
{
    public class NasaData
    {
        public Links links { get; set; }
        public int element_count { get; set; }
        public Dictionary<string, List<NearEarthObjects>> near_earth_objects { get; set; }
    }

    public class Links
    {
        public string next { get; set; }
        public string prev { get; set; }
        public string self { get; set; }

    }

    public class NearEarthObjects
    {
        public Links links { get; set; }
        public string id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string nasa_jpl_url { get; set; }
        public double absolute_magnitude_h { get; set; }
        public Dictionary<string, MinMax>? estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }

        public IList<CloseApproachData> close_approach_data { get; set; }
        public bool is_sentry_object { get; set; }
    }

    public class MinMax
    {
        public double estimated_diameter_min { get; set; }
        public double estimated_diameter_max { get; set; }
    }

    public class CloseApproachData
    {
        public string close_approach_date { get; set; }
        public string close_approach_date_full { get; set; }
        public long epoch_date_close_approach { get; set; }
        public Velocity relative_velocity { get; set; }
        public Distance miss_distance { get; set; }
        public string orbiting_body { get; set; }
    }

    public class Velocity
    {
        public string kilometers_per_second { get; set; }
        public string kilometers_per_hour { get; set; }
        public string miles_per_hour { get; set; }
    }

    public class Distance
    {
        public string astronomical { get; set; }
        public string lunar { get; set; }
        public string kilometers { get; set; }
        public string miles { get; set; }
    }
}