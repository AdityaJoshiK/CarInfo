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
using CarInfo.Areas.CAR_Review.Models;

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

            DataTable MakePhotoDataTable = carDal.PR_Client_AllMakerPhotos();
            DataTable makeDataTable = carDal.PR_Client_SelectRecentCars();

            CAR_ReviewModel model = new CAR_ReviewModel();
            DataTable reviewDataTable = dalCar.PR_CAR_Review_SelectAll(model);

            // Create a ViewModel and populate it
            var viewModel = new Car_Home
            {
                CarList = new List<CLIENT_Model>(), // Populate this with your CAR_List data
                MakeDataTable = makeDataTable,
                MakerPhotoDataTable = MakePhotoDataTable,
                ReviewDataTable = reviewDataTable
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

        [ClientCheckAccess]
        public IActionResult AddToFavourite(CLIENT_Model mmodel,string Home)
        {
            CLIENT_DALBase carDal = new CLIENT_DALBase();
            DataTable favourite = carDal.PR_Client_Favourite_Insert(mmodel);

            if(Home=="Home")
            {
                return RedirectToAction("GetFavouriteCars");
            }
            return RedirectToAction("Details", "ClientHome", new { CarID = mmodel.CarID });
        }

        [ClientCheckAccess]
        public IActionResult GetFavouriteCars()
        {
            CLIENT_DALBase carDal = new CLIENT_DALBase();
            DataTable favourite = carDal.PR_Client_Favourites();

            return View("Client_Favourites", favourite);
        }

        [ClientCheckAccess]
        public IActionResult DeleteFavouriteCar(int carID)
        {
            CLIENT_DALBase carDal = new CLIENT_DALBase();
            DataTable favourite = carDal.PR_Client_DeleteFavourite(carID);

            return RedirectToAction("GetFavouriteCars");
        }

        public IActionResult Compare()
        {
            return View("CAR_Compare");
        }

        public IActionResult Details(int CarID)
        {
            DataTable make = new DataTable();
            CLIENT_DALBase carDal = new CLIENT_DALBase();

            make = carDal.PR_Client_Car_Detail(CarID);

            DataTable reviews = carDal.PR_Client_Car_ReviewsByCarID(CarID);
            int numberOfReviews = reviews.Rows.Count;

            var viewModel = new CLIENT_Model
            {
               CarDetail=make,
               CarReviews=reviews,
               NumberOfReviews = numberOfReviews
            };

            return View("CAR_Detail",viewModel);
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

        public IActionResult SaveReview(CLIENT_Model mmodel)
        {
            #region Insert & Update

            // Create a new instance of CAR_ReviewModel and populate it with the form data
            CAR_ReviewModel model = new CAR_ReviewModel();
            model.CarID = mmodel.CarID;
            model.Rating = mmodel.Rating;
            model.ReviewText = mmodel.ReviewText;
            //model.Author = author;
            //model.Email = email;

            // Call the data access layer method to insert the new record
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (model.ReviewID == null || model.ReviewID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_Review_Insert(model);
                TempData["ReviewInsertMsg"] = "Record Inserted Successfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_Review_UpdateByPK(model);
                TempData["ReviewInsertMsg"] = "Record Updated Successfully";
            }

            return RedirectToAction("Details", "ClientHome", new { CarID = mmodel.CarID });

            #endregion
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
