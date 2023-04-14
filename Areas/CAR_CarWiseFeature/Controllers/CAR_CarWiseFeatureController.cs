using CarInfo.Areas.CAR_CarWiseFeature.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace CarInfo.Areas.CAR_CarWiseFeature.Controllers
{
    [Area("CAR_CarWiseFeature")]
    [Route("CAR_CarWiseFeature/[controller]/[action]")]
    public class CAR_CarWiseFeatureController : Controller
    {
        private IConfiguration Configuration;

        public CAR_CarWiseFeatureController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index(CAR_CarWiseFeatureModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable feature = new DataTable();
            CAR_DALBase dalCar = new CAR_DALBase();

            feature = dalCar.PR_CAR_CarWiseFeature_SelectAll(model);

            #region CarDropdown
            List<MST_CarDropDownModel> carList = dalCar.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            return View("CAR_CarWiseFeatureList", feature);
        }

        public IActionResult Save(CAR_CarWiseFeatureModel modelCAR_CarWiseFeature, List<string> FeatureNames)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("MyConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

                if (modelCAR_CarWiseFeature.FeatureID == null || modelCAR_CarWiseFeature.FeatureID == 0)
                {
                    // Insert new record
                    // Insert the first feature
                    modelCAR_CarWiseFeature.FeatureName = FeatureNames[0];
                    dalCAR.PR_CAR_CarWiseFeature_Insert(modelCAR_CarWiseFeature);

                    // Insert the remaining FeatureNames
                    for (int i = 1; i < FeatureNames.Count; i++)
                    {
                        CAR_CarWiseFeatureModel newFeature = new CAR_CarWiseFeatureModel
                        {
                            CarID = modelCAR_CarWiseFeature.CarID,
                            FeatureName = FeatureNames[i],
                            //UserID = modelCAR_CarWiseFeature.UserID
                        };
                        dalCAR.PR_CAR_CarWiseFeature_Insert(newFeature);
                    }

                    TempData["FeatureInsertMsg"] = "Record Inserted Succesfully";
                }
                else
                {
                    // Update existing record
                    modelCAR_CarWiseFeature.FeatureName = FeatureNames[0];
                    dalCAR.PR_CAR_CarWiseFeature_UpdateByPK(modelCAR_CarWiseFeature);

                    // Delete existing feature names for the record
                    //dalCAR.PR_CAR_CarWiseFeature_DeleteFeaturesByFeatureId(modelCAR_CarWiseFeature.FeatureID);

                    // Insert new feature names for the record
                    //foreach (string featureName in FeatureNames)
                    //{
                    //    CAR_CarWiseFeatureModel newFeature = new CAR_CarWiseFeatureModel
                    //    {
                    //        CarID = modelCAR_CarWiseFeature.CarID,
                    //        FeatureName = featureName,
                    //        FeatureID = modelCAR_CarWiseFeature.FeatureID,
                    //        //UserID = modelCAR_CarWiseFeature.UserID
                    //    };
                    //    dalCAR.PR_CAR_CarWiseFeature_Insert(newFeature);
                    //}

                    TempData["FeatureInsertMsg"] = "Record Updated Succesfully";
                }
            }
            catch (Exception ex)
            {
                TempData["FeatureInsertMsg"] = "Error occurred while saving record. " + ex.Message;
            }

            return RedirectToAction("Index");
        }



        public IActionResult Add(int? FeatureID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            #region CarDropdown
            List<MST_CarDropDownModel> carList = dalCAR.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            #region SelectByPK
            if (FeatureID != null)
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_CarWiseFeature_SelectByPK(str, FeatureID);
                CAR_CarWiseFeatureModel modelCAR_CarWiseFeature = new CAR_CarWiseFeatureModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_CarWiseFeature.FeatureID = Convert.ToInt32(dr["FeatureID"]);
                    modelCAR_CarWiseFeature.FeatureName = dr["FeatureName"].ToString();
                    modelCAR_CarWiseFeature.CarID = Convert.ToInt32(dr["CarID"]);
                }


                return View("CAR_CarWiseFeatureAddEdit", modelCAR_CarWiseFeature);
            }
            #endregion

            return View("CAR_CarWiseFeatureAddEdit");
        }

        #region Delete
        public IActionResult Delete(int FeatureID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_CarWiseFeature_DeleteByPK(FeatureID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
