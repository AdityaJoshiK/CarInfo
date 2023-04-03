using CarInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;

namespace CarInfo.Controllers
{
    public class CAR_FeatureController : Controller
    {
        private IConfiguration Configuration;
        public CAR_FeatureController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("myConnectionString");
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

            //List<CarFeature> carFeatures = carFeatureRepository.GetAllCarFeatures();
            return View(carFeatures);
        }
    }
}
