using CarInfo.Areas.CAR_TransmissionType.Models;
using CarInfo.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
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

            make = carDal.PR_CAR_TransmissionType_SelectAll(model);

            return View("CAR_TransmissionTypeList", make);
        }

        public IActionResult Save(CAR_TransmissionTypeModel modelCAR_TransmissionType)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_TransmissionType.TransmissionTypeID == null || modelCAR_TransmissionType.TransmissionTypeID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_TransmissionType_Insert(modelCAR_TransmissionType);
                TempData["TransmissionTypeInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_TransmissionType_UpdateByPK(modelCAR_TransmissionType);
                TempData["TransmissionTypeInsertMsg"] = "Record Updated Succesfully";
            }

            return RedirectToAction("Index");

            #endregion
        }

        public IActionResult Add(int? TransmissionTypeID)
        {
            #region SelectByPK
            if (TransmissionTypeID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");

                CAR_DAL dalCAR = new CAR_DAL();
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_TransmissionType_SelectByPK(TransmissionTypeID);
                CAR_TransmissionTypeModel modelCAR_TransmissionType = new CAR_TransmissionTypeModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_TransmissionType.TransmissionTypeID = Convert.ToInt32(dr["TransmissionTypeID"]);
                    modelCAR_TransmissionType.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                }


                return View("CAR_TransmissionTypeAddEdit", modelCAR_TransmissionType);
            }
            #endregion

            return View("CAR_TransmissionTypeAddEdit");
        }

        #region Delete
        public IActionResult Delete(int TransmissionTypeID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_TransmissionType_DeleteByPK(TransmissionTypeID);

            return RedirectToAction("Index");
        }
        #endregion
    }
}
