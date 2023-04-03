using System.Collections.Generic;
using System.Data.SqlClient;
using CarInfo.Models;
using Microsoft.Extensions.Configuration;


namespace CarInfo.Controllers
{

    public class CarFeatureRepository
    {
        private readonly string connectionString;

        public CarFeatureRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Car_Feature> GetAllCarFeatures()
        {
            List<Car_Feature> carFeatures = new List<Car_Feature>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Car_Feature", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Car_Feature carFeature = new Car_Feature();
                    carFeature.FeatureID = (int)reader["FeatureID"];
                    carFeature.FeatureName = reader["FeatureName"].ToString();
                    carFeatures.Add(carFeature);
                }

                reader.Close();
            }

            return carFeatures;
        }
    }

}
