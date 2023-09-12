using CarInfo.Areas.CAR_FuelType.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_FuelType.Controllers
{
    [CheckAccess]
    [Area("CAR_FuelType")]
    [Route("CAR_FuelType/[controller]/[action]")]
    public class CAR_FuelTypeController : Controller
    {
        private IConfiguration Configuration;

        public CAR_FuelTypeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_FuelTypeModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable fueltype = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            fueltype = carDal.PR_CAR_FuelType_SelectAll(model);

            return View("CAR_FuelTypeList", fueltype);
        }

        public IActionResult Save(CAR_FuelTypeModel modelCAR_FuelType)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_FuelType.FuelTypeID == null || modelCAR_FuelType.FuelTypeID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_FuelType_Insert(modelCAR_FuelType);
                TempData["FuelTypeInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_FuelType_UpdateByPK(modelCAR_FuelType);
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
                DataTable dt = dalCAR.dbo_PR_CAR_FuelType_SelectByPK(FuelTypeID);
                CAR_FuelTypeModel modelCAR_FuelType = new CAR_FuelTypeModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_FuelType.FuelTypeID = Convert.ToInt32(dr["FuelTypeID"]);
                    modelCAR_FuelType.FuelTypeName = dr["FuelTypeName"].ToString();
                }


                return View("CAR_FuelTypeAddEdit", modelCAR_FuelType);
            }
            #endregion

            return View("CAR_FuelTypeAddEdit");
        }

        #region Delete
        public IActionResult Delete(int FuelTypeID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_FuelType_DeleteByPK(FuelTypeID);

            return RedirectToAction("Index");
        }
        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
