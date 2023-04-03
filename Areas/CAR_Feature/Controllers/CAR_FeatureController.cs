using CarInfo.Areas.CAR_Feature.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CarInfo.Areas.CAR_Feature.Controllers
{
    [Area("CAR_Feature")]
    [Route("CAR_Feature/[controller]/[action]")]
    public class CAR_FeatureController : Controller
    {
        private IConfiguration Configuration;

        public CAR_FeatureController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index(CAR_FeatureModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable feature = new DataTable();
            CAR_DALBase dalCar = new CAR_DALBase();

            feature = dalCar.PR_CAR_Feature_SelectAll(model.FeatureName);

            return View("CAR_FeatureList",feature);
        }
    }
}
