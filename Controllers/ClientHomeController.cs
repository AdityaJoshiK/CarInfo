using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using CarInfo.DAL;
using CarInfo.BAL;
using System.Data;
using CarInfo.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.CAR_FuelType.Models;
using CarInfo.Areas.CAR_TransmissionType.Models;
using CarInfo.Areas.CAR_Type.Models;
using System.Data.SqlClient;

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

            CLIENT_DALBase carDal = new CLIENT_DALBase();
            CAR_DALBase dalCar = new CAR_DALBase();

            DataTable MakeDataTable = new DataTable();

            DataTable makeDataTable = carDal.PR_Client_SelectRecentCars();

            // Create a ViewModel and populate it
            var viewModel = new Car_Home
            {
                CarList = new List<CLIENT_Model>(), // Populate this with your CAR_List data
                MakeDataTable = makeDataTable
            };

            #region CarDropdown
            List<MST_CarDropDownModel> list1 = dalCar.PR_MST_Car_DropDown();
            ViewBag.CarList = list1;
            #endregion

            #region makeDropdown
            List<CAR_MakeDropDownModel> makeList = dalCar.PR_CAR_Make_DropDown();
            ViewBag.MakeList = makeList;
            #endregion

            #region TypeDropdown
            List<CAR_TypeDropDownModel> TypeList = dalCar.PR_CAR_Type_DropDown();
            ViewBag.TypeList = TypeList;
            #endregion

            #region FuelTypeDropdown
            List<CAR_FuelTypeDropDownModel> FuelTypeList = dalCar.PR_CAR_FuelType_DropDown();
            ViewBag.FuelTypeList = FuelTypeList;
            #endregion

            #region TransmissionTypeDropdown
            List<CAR_TransmissionTypeDropDownModel> TransmissionTypeList = dalCar.PR_CAR_TransmissionType_DropDown();
            ViewBag.TransmissionTypeList = TransmissionTypeList;
            #endregion

            return View("Index", viewModel);
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

            CAR_DALBase dalCar = new CAR_DALBase();

            #region CarDropdown
            List<MST_CarDropDownModel> list1 = dalCar.PR_MST_Car_DropDown();
            ViewBag.CarList = list1;
            #endregion

            #region makeDropdown
            List<CAR_MakeDropDownModel> makeList = dalCar.PR_CAR_Make_DropDown();
            ViewBag.MakeList = makeList;
            #endregion

            #region TypeDropdown
            List<CAR_TypeDropDownModel> TypeList = dalCar.PR_CAR_Type_DropDown();
            ViewBag.TypeList = TypeList;
            #endregion

            #region FuelTypeDropdown
            List<CAR_FuelTypeDropDownModel> FuelTypeList = dalCar.PR_CAR_FuelType_DropDown();
            ViewBag.FuelTypeList = FuelTypeList;
            #endregion

            #region TransmissionTypeDropdown
            List<CAR_TransmissionTypeDropDownModel> TransmissionTypeList = dalCar.PR_CAR_TransmissionType_DropDown();
            ViewBag.TransmissionTypeList = TransmissionTypeList;
            #endregion

            return View("CAR_List", viewModel);
        }

        public IActionResult CarFilter(CLIENT_Model model)
        {
            CLIENT_DALBase carDal = new CLIENT_DALBase();

            if (model.FuelTypeName != null && model.FuelTypeName == "Select FuelType")
            {
                model.FuelTypeName = null;
            }

            if (model.TransmissionTypeName != null && model.TransmissionTypeName == "Select TransmissionType")
            {
                model.TransmissionTypeName = null;
            }



            DataTable carCategories = carDal.PR_Client_Car_Categories();
            DataTable Filter = carDal.PR_Client_Filter(model);

            CLIENT_Model viewModel = new CLIENT_Model
            {
                CarCategories= carCategories,
                CarsByType = Filter
            };

            CAR_DALBase dalCar = new CAR_DALBase();

            #region CarDropdown
            List<MST_CarDropDownModel> list1 = dalCar.PR_MST_Car_DropDown();
            ViewBag.CarList = list1;
            #endregion

            #region makeDropdown
            List<CAR_MakeDropDownModel> makeList = dalCar.PR_CAR_Make_DropDown();
            ViewBag.MakeList = makeList;
            #endregion

            #region TypeDropdown
            List<CAR_TypeDropDownModel> TypeList = dalCar.PR_CAR_Type_DropDown();
            ViewBag.TypeList = TypeList;
            #endregion

            #region FuelTypeDropdown
            List<CAR_FuelTypeDropDownModel> FuelTypeList = dalCar.PR_CAR_FuelType_DropDown();
            ViewBag.FuelTypeList = FuelTypeList;
            #endregion

            #region TransmissionTypeDropdown
            List<CAR_TransmissionTypeDropDownModel> TransmissionTypeList = dalCar.PR_CAR_TransmissionType_DropDown();
            ViewBag.TransmissionTypeList = TransmissionTypeList;
            #endregion

            return View("CAR_List",viewModel);
        }

        public IActionResult DropDownByMakeCar(int MakeID, List<MST_CarDropDownModel> Make_list)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_MST_Car_SelectForDropDownMakeID";
            cmd1.Parameters.AddWithValue("@MakeID", MakeID);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt.Load(sdr1);
            conn.Close();

            List<MST_CarDropDownModel> list1 = new List<MST_CarDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                MST_CarDropDownModel vlst1 = new MST_CarDropDownModel();
                vlst1.CarID = Convert.ToInt32(dr["CarID"]);
                vlst1.Name = dr["Name"].ToString();
                Make_list.Add(vlst1);
            }

            var vModel = Make_list;
            return Json(vModel);
        }
    }
}
