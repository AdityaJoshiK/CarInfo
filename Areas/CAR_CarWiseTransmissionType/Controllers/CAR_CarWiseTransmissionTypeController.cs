using CarInfo.Areas.CAR_CarWiseTransmissionType.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;

namespace CarInfo.Areas.CAR_CarWiseTransmissionType.Controllers
{
    [Area("CAR_CarWiseTransmissionType")]
    [Route("CAR_CarWiseTransmissionType/[controller]/[action]")]
    public class CAR_CarWiseTransmissionTypeController : Controller
    {
        private IConfiguration Configuration;

        public CAR_CarWiseTransmissionTypeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_CarWiseTransmissionTypeModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable make = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            make = carDal.PR_CAR_CarWiseTransmissionType_SelectAll(model);

            #region CarDropdown
            List<MST_CarDropDownModel> carList = carDal.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            return View("CAR_CarWiseTransmissionTypeList", make);
        }

        public IActionResult Save(CAR_CarWiseTransmissionTypeModel modelCAR_CarWiseTransmissionType)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_CarWiseTransmissionType.TransmissionTypeID == null || modelCAR_CarWiseTransmissionType.TransmissionTypeID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_CarWiseTransmissionType_Insert(modelCAR_CarWiseTransmissionType);
                TempData["TransmissionTypeInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_CarWiseTransmissionType_UpdateByPK(modelCAR_CarWiseTransmissionType);
                TempData["TransmissionTypeInsertMsg"] = "Record Updated Succesfully";
            }

            return RedirectToAction("Index");

            #endregion
        }

        public IActionResult Add(int? TransmissionTypeID)
        {
            CAR_DAL dalCAR = new CAR_DAL();
            #region CarDropdown
            List<MST_CarDropDownModel> carList = dalCAR.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            #region SelectByPK
            if (TransmissionTypeID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");

                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_CarWiseTransmissionType_SelectByPK(TransmissionTypeID);
                CAR_CarWiseTransmissionTypeModel modelCAR_CarWiseTransmissionType = new CAR_CarWiseTransmissionTypeModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_CarWiseTransmissionType.TransmissionTypeID = Convert.ToInt32(dr["TransmissionTypeID"]);
                    modelCAR_CarWiseTransmissionType.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                }


                return View("CAR_CarWiseTransmissionTypeAddEdit", modelCAR_CarWiseTransmissionType);
            }
            #endregion

            return View("CAR_CarWiseTransmissionTypeAddEdit");
        }

        #region Delete
        public IActionResult Delete(int TransmissionTypeID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_CarWiseTransmissionType_DeleteByPK(TransmissionTypeID);

            return RedirectToAction("Index");
        }
        #endregion
    }
}
