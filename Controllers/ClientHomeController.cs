using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using CarInfo.DAL;
using System.Data;

namespace CarInfo.Controllers
{
    public class ClientHomeController : Controller
    {
        private IConfiguration Configuration;

        public ClientHomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            
            DataTable make = new DataTable();
            CLIENT_DALBase carDal = new CLIENT_DALBase();
    

            make = carDal.PR_Client_SelectRecentCars();

            //return View("Index", make);
            return View("CAR_List");
        }

        public IActionResult Details(int CarID)
        {
            DataTable make = new DataTable();
            CLIENT_DALBase carDal = new CLIENT_DALBase();

            make = carDal.PR_Client_Car_Detail(CarID);

            return View("CAR_Detail",make);
        }
    }
}
