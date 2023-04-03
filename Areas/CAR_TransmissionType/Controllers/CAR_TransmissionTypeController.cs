using CarInfo.Areas.CAR_TransmissionType.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_TransmissionType.Controllers
{
    [Area("CAR_TransmissionType")]
    [Route("CAR_TransmissionType/[controller]/[action]")]
    public class CAR_TransmissionTypeController : Controller
    {
        private IConfiguration Configuration;

        public CAR_TransmissionTypeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_TransmissionTypeModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable make = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            make = carDal.PR_CAR_TransmissionType_SelectAll();

            return View("CAR_TransmissionTypeList", make);
        }
    }
}
