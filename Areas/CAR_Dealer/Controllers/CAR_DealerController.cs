using CarInfo.Areas.CAR_Dealer.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_Dealer.Controllers
{
    [Area("CAR_Dealer")]
    [Route("CAR_Dealer/[controller]/[action]")]
    public class CAR_DealerController : Controller
    {
        private IConfiguration Configuration;

        public CAR_DealerController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_DealerModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");
                
            DataTable make = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            make = carDal.PR_CAR_Dealer_SelectAll(model.Name);

            return View("CAR_DealerList", make);
        }
    }
}
