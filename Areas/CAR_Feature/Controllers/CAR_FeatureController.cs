using CarInfo.Areas.CAR_Feature.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace CarInfo.Areas.CAR_Feature.Controllers
{
    [CheckAccess]
    [Area("CAR_Feature")]
    [Route("CAR_Feature/[controller]/[action]")]
    public class CAR_FeatureController : Controller
    {
        private IConfiguration Configuration;

        public CAR_FeatureController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index(CAR_FeatureModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable feature = new DataTable();
            CAR_DALBase dalCar = new CAR_DALBase();

            feature = dalCar.PR_CAR_Feature_SelectAll(model);


            return View("CAR_FeatureList",feature);
        }

        public IActionResult Save(CAR_FeatureModel modelCAR_Feature, List<string> FeatureNames)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("MyConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

                if (modelCAR_Feature.FeatureID == null || modelCAR_Feature.FeatureID == 0)
                {
                    // Insert new record
                    // Insert the first feature
                    modelCAR_Feature.FeatureName = FeatureNames[0];
                    dalCAR.PR_CAR_Feature_Insert(modelCAR_Feature);

                    // Insert the remaining FeatureNames
                    for (int i = 1; i < FeatureNames.Count; i++)
                    {
                        CAR_FeatureModel newFeature = new CAR_FeatureModel
                        {
                            FeatureName = FeatureNames[i],
                            //UserID = modelCAR_Feature.UserID
                        };
                        dalCAR.PR_CAR_Feature_Insert(newFeature);
                    }

                    TempData["FeatureInsertMsg"] = "Record Inserted Succesfully";
                }
                else
                {
                    // Update existing record
                    modelCAR_Feature.FeatureName = FeatureNames[0];
                    dalCAR.PR_CAR_Feature_UpdateByPK(modelCAR_Feature);

                    // Delete existing feature names for the record
                    //dalCAR.PR_CAR_Feature_DeleteFeaturesByFeatureId(modelCAR_Feature.FeatureID);

                    // Insert new feature names for the record
                    //foreach (string featureName in FeatureNames)
                    //{
                    //    CAR_FeatureModel newFeature = new CAR_FeatureModel
                    //    {
                    //        CarID = modelCAR_Feature.CarID,
                    //        FeatureName = featureName,
                    //        FeatureID = modelCAR_Feature.FeatureID,
                    //        //UserID = modelCAR_Feature.UserID
                    //    };
                    //    dalCAR.PR_CAR_Feature_Insert(newFeature);
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

            #region SelectByPK
            if (FeatureID != null)
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_Feature_SelectByPK(str, FeatureID);
                CAR_FeatureModel modelCAR_Feature = new CAR_FeatureModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_Feature.FeatureID = Convert.ToInt32(dr["FeatureID"]);
                    modelCAR_Feature.FeatureName = dr["FeatureName"].ToString();
                }


                return View("CAR_FeatureAddEdit", modelCAR_Feature);
            }
            #endregion

            return View("CAR_FeatureAddEdit");
        }

        #region Delete
        public IActionResult Delete(int FeatureID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_Feature_DeleteByPK(FeatureID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
