using CarInfo.Areas.CAR_FuelType.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_FuelType.Controllers
{
    [Area("CAR_FuelType")]
    [Route("CAR_FuelType/[controller]/[action]")]
    public class CAR_FuelTypeController : Controller
    {
        private IConfiguration Configuration;

        public CAR_FuelTypeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_FuelTypeModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable fueltype = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            fueltype = carDal.PR_CAR_FuelType_SelectAll();

            return View("CAR_FuelTypeList", fueltype);
        }
    }
}
