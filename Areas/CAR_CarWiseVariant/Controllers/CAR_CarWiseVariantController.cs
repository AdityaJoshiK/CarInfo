using CarInfo.Areas.CAR_CarWiseVariant.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;

namespace CarInfo.Areas.CAR_CarWiseVariant.Controllers
{
    [Area("CAR_CarWiseVariant")]
    [Route("CAR_CarWiseVariant/[controller]/[action]")]
    public class CAR_CarWiseVariantController : Controller
    {
        private IConfiguration Configuration;

        public CAR_CarWiseVariantController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_CarWiseVariantModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable variants = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            variants = carDal.PR_CAR_CarWiseVariant_SelectAll(model);

            #region CarDropdown
            List<MST_CarDropDownModel> carList = carDal.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            return View("CAR_CarWiseVariantList", variants);
        }

        public IActionResult Save(CAR_CarWiseVariantModel modelCAR_CarWiseVariant, List<string> VariantNames)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("MyConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

                if (modelCAR_CarWiseVariant.VariantID == null || modelCAR_CarWiseVariant.VariantID == 0)
                {
                    // Insert new record
                    // Insert the first Variant
                    modelCAR_CarWiseVariant.VariantName = VariantNames[0];
                    dalCAR.PR_CAR_CarWiseVariant_Insert(modelCAR_CarWiseVariant);

                    // Insert the remaining VariantNames
                    for (int i = 1; i < VariantNames.Count; i++)
                    {
                        CAR_CarWiseVariantModel newVariant = new CAR_CarWiseVariantModel
                        {
                            CarID = modelCAR_CarWiseVariant.CarID,
                            VariantName = VariantNames[i],
                            //UserID = modelCAR_CarWiseVariant.UserID
                        };
                        dalCAR.PR_CAR_CarWiseVariant_Insert(newVariant);
                    }

                    TempData["VariantInsertMsg"] = "Record Inserted Succesfully";
                }
                else
                {
                    // Update existing record
                    modelCAR_CarWiseVariant.VariantName = VariantNames[0];
                    dalCAR.PR_CAR_CarWiseVariant_UpdateByPK(modelCAR_CarWiseVariant);

                    // Delete existing Variant names for the record
                    //dalCAR.PR_CAR_CarWiseVariant_DeleteVariantsByVariantId(modelCAR_CarWiseVariant.VariantID);

                    // Insert new Variant names for the record
                    //foreach (string VariantName in VariantNames)
                    //{
                    //    CAR_CarWiseVariantModel newVariant = new CAR_CarWiseVariantModel
                    //    {
                    //        CarID = modelCAR_CarWiseVariant.CarID,
                    //        VariantName = VariantName,
                    //        VariantID = modelCAR_CarWiseVariant.VariantID,
                    //        //UserID = modelCAR_CarWiseVariant.UserID
                    //    };
                    //    dalCAR.PR_CAR_CarWiseVariant_Insert(newVariant);
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

            #region CarDropdown
            List<MST_CarDropDownModel> carList = dalCAR.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            #region SelectByPK
            if (VariantID != null)
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_CarWiseVariant_SelectByPK(str, VariantID);
                CAR_CarWiseVariantModel modelCAR_CarWiseVariant = new CAR_CarWiseVariantModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_CarWiseVariant.VariantID = Convert.ToInt32(dr["VariantID"]);
                    modelCAR_CarWiseVariant.VariantName = dr["VariantName"].ToString();
                    modelCAR_CarWiseVariant.CarID = Convert.ToInt32(dr["CarID"]);
                }


                return View("CAR_CarWiseVariantAddEdit", modelCAR_CarWiseVariant);
            }
            #endregion

            return View("CAR_CarWiseVariantAddEdit");
        }

        #region Delete
        public IActionResult Delete(int VariantID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_CarWiseVariant_DeleteByPK(VariantID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
