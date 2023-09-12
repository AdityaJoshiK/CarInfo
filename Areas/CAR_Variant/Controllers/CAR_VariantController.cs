using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.CAR_Variant.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CarInfo.Areas.CAR_Variant.Controllers
{
    [CheckAccess]
    [Area("CAR_Variant")]
    [Route("CAR_Variant/[controller]/[action]")]
    public class CAR_VariantController : Controller
    {
        private IConfiguration Configuration;

        public CAR_VariantController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_VariantModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable variants = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            variants = carDal.PR_CAR_Variant_SelectAll(model);

            #region makeDropdown
            List<CAR_MakeDropDownModel> makeList = carDal.PR_CAR_Make_DropDown();
            ViewBag.MakeList = makeList;
            #endregion

            return View("CAR_VariantList", variants);
        }

        public IActionResult Save(CAR_VariantModel modelCAR_Variant, List<string> VariantNames)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("MyConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

                if (modelCAR_Variant.VariantID == null || modelCAR_Variant.VariantID == 0)
                {
                    // Insert new record
                    // Insert the first Variant
                    modelCAR_Variant.VariantName = VariantNames[0];
                    dalCAR.PR_CAR_Variant_Insert(modelCAR_Variant);

                    // Insert the remaining VariantNames
                    for (int i = 1; i < VariantNames.Count; i++)
                    {
                        CAR_VariantModel newVariant = new CAR_VariantModel
                        {
                            MakeID = modelCAR_Variant.MakeID,
                            VariantName = VariantNames[i],
                            //UserID = modelCAR_Variant.UserID
                        };
                        dalCAR.PR_CAR_Variant_Insert(newVariant);
                    }

                    TempData["VariantInsertMsg"] = "Record Inserted Succesfully";
                }
                else
                {
                    // Update existing record
                    modelCAR_Variant.VariantName = VariantNames[0];
                    dalCAR.PR_CAR_Variant_UpdateByPK(modelCAR_Variant);

                    // Delete existing Variant names for the record
                    //dalCAR.PR_CAR_Variant_DeleteVariantsByVariantId(modelCAR_Variant.VariantID);

                    // Insert new Variant names for the record
                    //foreach (string VariantName in VariantNames)
                    //{
                    //    CAR_VariantModel newVariant = new CAR_VariantModel
                    //    {
                    //        CarID = modelCAR_Variant.CarID,
                    //        VariantName = VariantName,
                    //        VariantID = modelCAR_Variant.VariantID,
                    //        //UserID = modelCAR_Variant.UserID
                    //    };
                    //    dalCAR.PR_CAR_Variant_Insert(newVariant);
                    //}

                    TempData["VariantInsertMsg"] = "Record Updated Succesfully";
                }
            }
            catch (Exception ex)
            {
                TempData["VariantInsertMsg"] = "Error occurred while saving record. " + ex.Message;
            }

            return RedirectToAction("Index");
        }



        public IActionResult Add(int? VariantID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            
            CAR_DAL dalCAR = new CAR_DAL();

            #region makeDropdown
            List<CAR_MakeDropDownModel> makeList = dalCAR.PR_CAR_Make_DropDown();
            ViewBag.MakeList = makeList;
            #endregion

            #region SelectByPK
            if (VariantID != null)
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_Variant_SelectByPK(str, VariantID);
                CAR_VariantModel modelCAR_Variant = new CAR_VariantModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_Variant.VariantID = Convert.ToInt32(dr["VariantID"]);
                    modelCAR_Variant.VariantName = dr["VariantName"].ToString();
                    //modelCAR_Variant.CarID = Convert.ToInt32(dr["CarID"]);
                }


                return View("CAR_VariantAddEdit", modelCAR_Variant);
            }
            #endregion

            return View("CAR_VariantAddEdit");
        }

        #region Delete
        public IActionResult Delete(int VariantID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_Variant_DeleteByPK(VariantID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
