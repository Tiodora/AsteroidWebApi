namespace AsteroidsWebApi.DTO
{
    public class Asteroids
    {
        public string name { get; set; }
        public double diameter { get; set; }
        public AsteroidApproachData close_approach_data { get; set; }
    }

    public class AsteroidApproachData
    {
        public string kilometers_per_hour { get; set; }
        public DateTime close_approach_date { get; set; }
        public string orbiting_body { get; set; }
    }

}
