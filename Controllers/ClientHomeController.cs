using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using CarInfo.DAL;
using System.Data;
using CarInfo.Models;

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

            return View("Index", make);
            //return View("CAR_List");
        }

        public IActionResult Details(int CarID)
        {
            DataTable make = new DataTable();
            CLIENT_DALBase carDal = new CLIENT_DALBase();

            make = carDal.PR_Client_Car_Detail(CarID);

            return View("CAR_Detail",make);
        }

        public IActionResult CarList(int TypeID,string FuelTypeName,string TransmissionTypeName)
        {
            CLIENT_DALBase carDal = new CLIENT_DALBase();

            DataTable carCategories = carDal.PR_Client_Car_Categories();
            DataTable carsByType = carDal.PR_Client_AllCars();

            if (TypeID != 0)
            {
                  carsByType = carDal.PR_Client_CarByType(TypeID);
            }

            else if (FuelTypeName != null)
            {
                carsByType = carDal.PR_Client_CarByFuelType(FuelTypeName);
            }

            else if (TransmissionTypeName != null)
            {
                carsByType = carDal.PR_Client_CarByTransmissionType(TransmissionTypeName);
            }

            CLIENT_Model viewModel = new CLIENT_Model
            {
                CarCategories = carCategories,
                CarsByType = carsByType
            };

            return View("CAR_List", viewModel);
        }
    }
}
