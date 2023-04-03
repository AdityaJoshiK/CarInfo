using CarInfo.Areas.MST_Car.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.MST_Car.Controllers
{
    [Area("MST_Car")]
    [Route("MST_Car/[controller]/[action]")]
    public class MST_CarController : Controller
    {
        private IConfiguration Configuration;

        public MST_CarController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(MST_CarModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable make = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            make = carDal.PR_MST_Car_SelectAll();

            return View("MST_CarList", make);
        }
    }
}
