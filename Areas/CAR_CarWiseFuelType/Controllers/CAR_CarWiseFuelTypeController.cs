using CarInfo.Areas.CAR_CarWiseFuelType.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_CarWiseFuelType.Controllers
{
    [CheckAccess]
    [Area("CAR_CarWiseFuelType")]
    [Route("CAR_CarWiseFuelType/[controller]/[action]")]
    public class CAR_CarWiseFuelTypeController : Controller
    {
        private IConfiguration Configuration;

        public CAR_CarWiseFuelTypeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_CarWiseFuelTypeModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable fueltype = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            fueltype = carDal.PR_CAR_CarWiseFuelType_SelectAll(model);

            return View("CAR_CarWiseFuelTypeList", fueltype);
        }

        public IActionResult Save(CAR_CarWiseFuelTypeModel modelCAR_CarWiseFuelType)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_CarWiseFuelType.FuelTypeID == null || modelCAR_CarWiseFuelType.FuelTypeID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_CarWiseFuelType_Insert(modelCAR_CarWiseFuelType);
                TempData["FuelTypeInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_CarWiseFuelType_UpdateByPK(modelCAR_CarWiseFuelType);
                TempData["FuelTypeInsertMsg"] = "Record Updated Succesfully";
            }

            return RedirectToAction("Index");

            #endregion
        }

        public IActionResult Add(int? FuelTypeID)
        {
            CAR_DAL dalCAR = new CAR_DAL();
            #region CarDropdown
            List<MST_CarDropDownModel> carList = dalCAR.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            #region SelectByPK
            if (FuelTypeID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");

                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_CarWiseFuelType_SelectByPK(FuelTypeID);
                CAR_CarWiseFuelTypeModel modelCAR_CarWiseFuelType = new CAR_CarWiseFuelTypeModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_CarWiseFuelType.FuelTypeID = Convert.ToInt32(dr["FuelTypeID"]);
                    modelCAR_CarWiseFuelType.FuelTypeName = dr["FuelTypeName"].ToString();
                }


                return View("CAR_CarWiseFuelTypeAddEdit", modelCAR_CarWiseFuelType);
            }
            #endregion

            return View("CAR_CarWiseFuelTypeAddEdit");
        }

        #region Delete
        public IActionResult Delete(int FuelTypeID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_CarWiseFuelType_DeleteByPK(FuelTypeID);

            return RedirectToAction("Index");
        }
        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
