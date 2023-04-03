using CarInfo.Areas.CAR_Variant.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_Variant.Controllers
{
    [Area("CAR_Variant")]
    [Route("CAR_Variant/[controller]/[action]")]
    public class CAR_VariantController : Controller
    {
        private IConfiguration Configuration;

        public CAR_VariantController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_VariantModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable variants = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            variants = carDal.PR_CAR_Variant_SelectAll();

            return View("CAR_VariantList", variants);
        }
    }
}
