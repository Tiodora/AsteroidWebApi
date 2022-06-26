namespace AsteroidsWebApi.DTO
{
    public class ConvertTo
    {
        public List<Asteroids> NasaDataToAsteroids(NasaData? nasaData)
        {
            try
            {
                List<Asteroids> asteroids = new List<Asteroids>();
                foreach (List<NearEarthObjects> nearEarthObjects in nasaData.near_earth_objects.Values)
                {
                    foreach (NearEarthObjects nearEarthObject in nearEarthObjects)
                    {
                        double diameter = (nearEarthObject.estimated_diameter["kilometers"].estimated_diameter_min + nearEarthObject.estimated_diameter["kilometers"].estimated_diameter_max) / 2;

                        AsteroidApproachData closeApproachData = new AsteroidApproachData();
                        closeApproachData.kilometers_per_hour = nearEarthObject.close_approach_data.First().relative_velocity.kilometers_per_hour;
                        closeApproachData.close_approach_date = Convert.ToDateTime(nearEarthObject.close_approach_data.First().close_approach_date);
                        closeApproachData.orbiting_body = nearEarthObject.close_approach_data.First().orbiting_body;

                        asteroids.Add(new Asteroids()
                        {
                            name = nearEarthObject.name,
                            diameter = diameter,
                            close_approach_data = closeApproachData
                        });
                    }
                }


                return asteroids;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ConvertTo error {e}");
                return null;
            }
        }
    }
}
