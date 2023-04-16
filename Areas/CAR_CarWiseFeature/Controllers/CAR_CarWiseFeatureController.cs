using CarInfo.Areas.CAR_CarWiseFeature.Models;
using CarInfo.Areas.CAR_Feature.Models;
using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.CAR_Variant.Models;
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

        public IActionResult Save(CAR_CarWiseFeatureModel modelCAR_CarWiseFeature, List<string> FeatureNames, List<string> NewFeatureNames)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("MyConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

                if (modelCAR_CarWiseFeature.FeatureID == null || modelCAR_CarWiseFeature.FeatureID == 0)
                {

                    #region Feature Add
                    // Insert the FeatureNames into a separate table
                    foreach (string featureName in FeatureNames)
                    {
                        CAR_CarWiseFeatureModel newFeature = new CAR_CarWiseFeatureModel
                        {
                            CarID = modelCAR_CarWiseFeature.CarID,
                            VariantID = modelCAR_CarWiseFeature.VariantID,
                            FeatureName = featureName
                        };
                        dalCAR.PR_CAR_CarWiseFeature_Insert(newFeature);
                    }
                    #endregion

                    #region Feature Add
                    // Insert the FeatureNames into a separate table
                    foreach (string featureName in NewFeatureNames)
                    {
                        CAR_CarWiseFeatureModel newFeature = new CAR_CarWiseFeatureModel
                        {
                            CarID = modelCAR_CarWiseFeature.CarID,
                            VariantID = modelCAR_CarWiseFeature.VariantID,
                            FeatureName = featureName
                        };
                        CAR_FeatureModel addFeature = new CAR_FeatureModel
                        {
                            FeatureName = featureName
                        };
                        dalCAR.PR_CAR_CarWiseFeature_Insert(newFeature);
                        dalCAR.PR_CAR_Feature_Insert(addFeature);
                    }
                    #endregion
                }

                else
                {
                    DataTable dt = dalCAR.PR_CAR_CarWiseFeature_UpdateByPK(modelCAR_CarWiseFeature);
                    TempData["FeatureInsertMsg"] = "Record Updated Successfully";
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

            #region makeDropdown
            List<CAR_MakeDropDownModel> makeList = dalCAR.PR_CAR_Make_DropDown();
            ViewBag.MakeList = makeList;
            #endregion

            #region CarDropdown
            List<MST_CarDropDownModel> carList = new List<MST_CarDropDownModel>();
            ViewBag.CarList = carList;
            #endregion

            #region FeatureDropdown
            List<CAR_FeatureDropDownModel> FeatureList = dalCAR.PR_CAR_Feature_DropDown();
            ViewBag.FeatureList = FeatureList;
            #endregion

            #region SelectByPK
            if (FeatureID != null)
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_CarWiseFeature_SelectByPK(str, FeatureID);
                CAR_CarWiseFeatureModel modelCAR_CarWiseFeature = new CAR_CarWiseFeatureModel();



                foreach (DataRow dr in dt.Rows)
                {
                    DropDownByMakeCar(Convert.ToInt32(dr["CarID"]), carList);
                    modelCAR_CarWiseFeature.FeatureID = Convert.ToInt32(dr["FeatureID"]);
                    modelCAR_CarWiseFeature.FeatureName = dr["FeatureName"].ToString();
                    modelCAR_CarWiseFeature.CarID = Convert.ToInt32(dr["CarID"]);
                    modelCAR_CarWiseFeature.VariantID = Convert.ToInt32(dr["VariantID"]);
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

        public IActionResult DropDownByMake(int CarID, List<CAR_VariantDropDownModel> variant_list)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_CAR_CarWiseVariant_SelectForDropDownByCarID";
            cmd1.Parameters.AddWithValue("@CarID", CarID);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt.Load(sdr1);
            conn.Close();

            List<CAR_VariantDropDownModel> list1 = new List<CAR_VariantDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                CAR_VariantDropDownModel vlst1 = new CAR_VariantDropDownModel();
                vlst1.VariantID = Convert.ToInt32(dr["VariantID"]);
                vlst1.VariantName = dr["VariantName"].ToString();
                variant_list.Add(vlst1);
            }

            var vModel = variant_list;
            return Json(vModel);
        }

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
