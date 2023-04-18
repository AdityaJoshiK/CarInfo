using CarInfo.DAL;
using CarInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
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
            CLIENT_DALBase carDal = new CLIENT_DALBase();

            DataTable carCategories = carDal.PR_Client_Car_Categories();
            DataTable carsByType = carDal.PR_Client_CarByType(7);

            Car_Feature viewModel = new Car_Feature
            {
                CarCategories = carCategories,
                CarsByType = carsByType
            };

            return View("Index", viewModel);
        }


    }
}
