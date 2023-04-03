using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_Review.Controllers
{
    [Area("CAR_Review")]
    [Route("CAR_Review/[controller]/[action]")]
    public class CAR_ReviewController : Controller
    {
        private IConfiguration Configuration;

        public CAR_ReviewController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable reviews = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            reviews = carDal.PR_CAR_Review_SelectAll();

            return View("CAR_ReviewList", reviews);
        }
    }
}
