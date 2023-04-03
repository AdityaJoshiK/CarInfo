using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_Type.Controllers
{
    [Area("CAR_Type")]
    [Route("CAR_Type/[controller]/[action]")]
    public class CAR_TypeController : Controller
    {
        private IConfiguration Configuration;

        public CAR_TypeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable Type = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            Type = carDal.PR_CAR_Type_SelectAll();

            return View("CAR_TypeList", Type);
        }
    }
}
