using Microsoft.Extensions.Logging;

namespace AsteroidsWebApi.DTO
{
    public class ConvertTo
    {
        public List<Asteroids>? NasaDataToAsteroids(NasaData? nasaData, string planet)
        {
            try
            {
                List<Asteroids> asteroids = new List<Asteroids>();
                foreach (List<NearEarthObjects> nearEarthObjects in nasaData.NearEarthObjects.Values)
                {
                    foreach (NearEarthObjects nearEarthObject in nearEarthObjects)
                    {
                        if (!nearEarthObject.IsHazardousAsteroid || !nearEarthObject.CloseApproachData.FirstOrDefault().OrbitingBody.ToUpper().Equals(planet)) continue;

                        double diameter = (nearEarthObject.AvgDiameter["kilometers"].MinDiameter + nearEarthObject.AvgDiameter["kilometers"].MaxDiameter) / 2;

                        AsteroidApproachData closeApproachData = new AsteroidApproachData();
                        closeApproachData.KilometersHour = nearEarthObject.CloseApproachData.FirstOrDefault().RelativeVelocity.KilometersHour;
                        closeApproachData.ApproachDate = Convert.ToDateTime(nearEarthObject.CloseApproachData.FirstOrDefault().ApproachDate);
                        closeApproachData.OrbitingBody = nearEarthObject.CloseApproachData.FirstOrDefault().OrbitingBody;

                        asteroids.Add(new Asteroids()
                        {
                            Name = nearEarthObject.Name,
                            Diameter = diameter,
                            ApproachData = closeApproachData
                        });
                    }
                }


                return asteroids.OrderByDescending(ast => ast.Diameter).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
