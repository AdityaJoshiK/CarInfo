using CarInfo.Areas.CAR_Feature.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace CarInfo.Areas.CAR_Feature.Controllers
{
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

            feature = dalCar.PR_CAR_Feature_SelectAll(model.FeatureName);

            return View("CAR_FeatureList",feature);
        }

        public IActionResult Save(CAR_FeatureModel modelCAR_Feature, List<string> FeatureNames)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("MyConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

                // Insert the first feature
                modelCAR_Feature.FeatureName = FeatureNames[0];
                dalCAR.PR_CAR_Feature_Insert(modelCAR_Feature);

                // Insert the remaining FeatureNames
                for (int i = 1; i < FeatureNames.Count; i++)
                {
                    CAR_FeatureModel newFeature = new CAR_FeatureModel
                    {
                        CarID = modelCAR_Feature.CarID,
                        FeatureName = FeatureNames[i],
                        //UserID = modelCAR_Feature.UserID
                    };
                    dalCAR.PR_CAR_Feature_Insert(newFeature);
                }

                TempData["FeatureInsertMsg"] = "Record Inserted Succesfully";
            }
            catch (Exception ex)
            {
                TempData["FeatureInsertMsg"] = "Error occurred while inserting record. " + ex.Message;
            }

            return RedirectToAction("Index");
        }


        public IActionResult Add(int? FeatureID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            #region CarDropdown
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            SqlCommand cmd = conn1.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Car_SelectForDropDown";
            //cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = CV.UserID();
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd.ExecuteReader();
            dt1.Load(sdr1);

            List<MST_CarDropDownModel> list = new List<MST_CarDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                MST_CarDropDownModel vlst = new MST_CarDropDownModel();
                vlst.CarID = Convert.ToInt32(dr["CarID"]);
                vlst.CarName = dr["Name"].ToString();
                list.Add(vlst);
            }
            ViewBag.CarList = list;
            #endregion

            #region SelectByPK
            if (FeatureID != null)
            {
                CAR_DAL dalCAR = new CAR_DAL();
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_Feature_SelectByPK(str, FeatureID);
                CAR_FeatureModel modelCAR_Feature = new CAR_FeatureModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_Feature.FeatureID = Convert.ToInt32(dr["FeatureID"]);
                    modelCAR_Feature.FeatureName = dr["FeatureName"].ToString();
                    modelCAR_Feature.CarID = Convert.ToInt32(dr["CarID"]);
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
